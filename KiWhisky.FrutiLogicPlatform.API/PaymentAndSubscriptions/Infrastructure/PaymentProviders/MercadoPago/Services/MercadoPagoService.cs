using System.Text.Json;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Application.Internal.OutBoundServices.PaymentProviders.models;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Application.Internal.OutBoundServices.PaymentProviders.services;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.PaymentProviders.MercadoPago.Configuration;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using Microsoft.Extensions.Options;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.PaymentProviders.MercadoPago.Services;

/// <summary>
///     MercadoPago Checkout Pro service implementation.
/// </summary>
public class MercadoPagoService : IMercadoPagoService
{
    private readonly MercadoPagoSettings _settings;
    private readonly ILogger<MercadoPagoService> _logger;

    public MercadoPagoService(IOptions<MercadoPagoSettings> settings, ILogger<MercadoPagoService> logger)
    {
        _settings = settings.Value;
        _logger = logger;

        if (string.IsNullOrWhiteSpace(_settings.AccessToken) ||
            _settings.AccessToken.Contains("YOUR_ACCESS_TOKEN", StringComparison.OrdinalIgnoreCase) ||
            _settings.AccessToken.Contains("ACCESS_TOKEN_HERE", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("MercadoPago AccessToken is not configured. Paid plan checkout will fail until it is set.");
        }
        else
        {
            MercadoPagoConfig.AccessToken = _settings.AccessToken;
        }
    }

    public (string PreferenceId, string InitPoint) CreatePaymentPreference(
        string title,
        decimal price,
        string currency,
        int quantity,
        string accountId,
        DateTime? expirationDateFrom,
        DateTime? expirationDateTo)
    {
        if (string.IsNullOrWhiteSpace(_settings.AccessToken) ||
            _settings.AccessToken.Contains("YOUR_ACCESS_TOKEN", StringComparison.OrdinalIgnoreCase) ||
            _settings.AccessToken.Contains("ACCESS_TOKEN_HERE", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Mercado Pago Access Token is not configured. Set MercadoPagoSettings__AccessToken in the environment.");
        }

        MercadoPagoConfig.AccessToken = _settings.AccessToken;

        var frontendBase = ResolveFrontendBaseUrl();
        var backendBase = NormalizeBaseUrl(_settings.ResolvedBackendPublicUrl)
                          ?? "https://frutilogic-platform-api-production.up.railway.app";

        // If WebhookUrl already points to the subscriptions endpoint, use it as-is.
        var notificationUrl = _settings.WebhookUrl?.Contains("/api/v1/subscriptions", StringComparison.OrdinalIgnoreCase) == true
            ? _settings.WebhookUrl.Trim()
            : $"{backendBase}/api/v1/subscriptions";

        var request = new PreferenceRequest
        {
            Items =
            [
                new PreferenceItemRequest
                {
                    Title = title,
                    Quantity = quantity,
                    CurrencyId = string.IsNullOrWhiteSpace(currency) ? "PEN" : currency,
                    UnitPrice = price
                }
            ],
            BackUrls = new PreferenceBackUrlsRequest
            {
                Success = $"{frontendBase}/payments-success",
                Failure = $"{frontendBase}/payments-cancel",
                Pending = $"{frontendBase}/payments-success"
            },
            NotificationUrl = notificationUrl,
            AutoReturn = "approved",
            ExternalReference = accountId,
            Metadata = new Dictionary<string, object>
            {
                { "account_id", accountId }
            },
            Expires = expirationDateFrom.HasValue || expirationDateTo.HasValue,
            ExpirationDateFrom = expirationDateFrom,
            ExpirationDateTo = expirationDateTo
        };

        try
        {
            var client = new PreferenceClient();
            var preference = client.CreateAsync(request).GetAwaiter().GetResult();

            var useSandbox = _settings.IsSandboxMode;
            var checkoutUrl = useSandbox
                ? (preference.SandboxInitPoint ?? preference.InitPoint)
                : (preference.InitPoint ?? preference.SandboxInitPoint);

            if (string.IsNullOrWhiteSpace(checkoutUrl))
            {
                throw new InvalidOperationException(
                    "Mercado Pago did not return a checkout URL. Check AccessToken and UseSandbox.");
            }

            _logger.LogInformation(
                "Created Mercado Pago preference {PreferenceId} for account {AccountId} (sandbox={UseSandbox})",
                preference.Id,
                accountId,
                useSandbox);

            return (preference.Id, checkoutUrl);
        }
        catch (Exception ex) when (ex is not InvalidOperationException)
        {
            _logger.LogError(ex, "Mercado Pago CreatePaymentPreference failed for account {AccountId}", accountId);
            throw new InvalidOperationException(
                $"Mercado Pago preference failed: {ex.Message}", ex);
        }
    }

    private string ResolveFrontendBaseUrl()
    {
        var configured = NormalizeBaseUrl(_settings.FrontendPublicUrl);
        if (IsUsablePublicUrl(configured))
            return configured!;

        var fromEnv = Environment.GetEnvironmentVariable("FRONTEND_URL");
        if (!string.IsNullOrWhiteSpace(fromEnv))
        {
            // FRONTEND_URL may be a comma-separated CORS list; take the first.
            var first = fromEnv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .FirstOrDefault();
            var normalized = NormalizeBaseUrl(first);
            if (IsUsablePublicUrl(normalized))
                return normalized!;
        }

        return "https://stocksip-front-end-application.vercel.app";
    }

    private static bool IsUsablePublicUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;
        if (url.Contains("TU-FRONTEND", StringComparison.OrdinalIgnoreCase))
            return false;
        if (url.Contains("YOUR_", StringComparison.OrdinalIgnoreCase))
            return false;
        if (url.Contains("localhost", StringComparison.OrdinalIgnoreCase))
            return false;
        return Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
               (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp);
    }

    public async Task<MercadoPagoPayment?> GetPaymentById(string paymentId)
    {
        if (!long.TryParse(paymentId, out var id))
            throw new ArgumentException("Invalid payment ID", nameof(paymentId));

        MercadoPagoConfig.AccessToken = _settings.AccessToken;
        var client = new PaymentClient();
        var payment = await client.GetAsync(id);

        var accountId = "";

        if (payment.Metadata is IDictionary<string, object> dict)
        {
            if (dict.TryGetValue("account_id", out var value))
                accountId = value?.ToString() ?? "";
        }
        else if (payment.Metadata != null)
        {
            try
            {
                var json = payment.Metadata.ToString();
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("account_id", out var accElem))
                    accountId = accElem.GetString() ?? "";
            }
            catch
            {
                throw new Exception("Invalid payment metadata");
            }
        }

        if (string.IsNullOrWhiteSpace(accountId) && !string.IsNullOrWhiteSpace(payment.ExternalReference))
            accountId = payment.ExternalReference;

        return new MercadoPagoPayment(
            payment.Id.ToString(),
            payment.Status,
            accountId
        );
    }

    private static string? NormalizeBaseUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return null;

        return url.Trim().TrimEnd('/');
    }
}
