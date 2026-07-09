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

        var frontendBase = NormalizeBaseUrl(_settings.FrontendPublicUrl)
                           ?? "https://frutilogic-frontend.vercel.app";
        var backendBase = NormalizeBaseUrl(_settings.BackendPublicUrl)
                          ?? "https://frutilogic-platform-api-production.up.railway.app";

        var request = new PreferenceRequest
        {
            Items =
            [
                new PreferenceItemRequest
                {
                    Title = title,
                    Quantity = quantity,
                    CurrencyId = currency,
                    UnitPrice = price
                }
            ],
            BackUrls = new PreferenceBackUrlsRequest
            {
                Success = $"{frontendBase}/payments-success",
                Failure = $"{frontendBase}/payments-cancel",
                Pending = $"{frontendBase}/payments-success"
            },
            NotificationUrl = $"{backendBase}/api/v1/subscriptions",
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

        var client = new PreferenceClient();
        var preference = client.CreateAsync(request).GetAwaiter().GetResult();

        var checkoutUrl = _settings.UseSandbox
            ? (preference.SandboxInitPoint ?? preference.InitPoint)
            : (preference.InitPoint ?? preference.SandboxInitPoint);

        _logger.LogInformation(
            "Created Mercado Pago preference {PreferenceId} for account {AccountId} (sandbox={UseSandbox})",
            preference.Id,
            accountId,
            _settings.UseSandbox);

        return (preference.Id, checkoutUrl);
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
