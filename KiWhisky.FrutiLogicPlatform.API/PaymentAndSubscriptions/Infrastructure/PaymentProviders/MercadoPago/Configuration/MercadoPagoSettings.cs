namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.PaymentProviders.MercadoPago.Configuration;

/// <summary>
///     Configuration settings for MercadoPago Checkout Pro.
/// </summary>
public class MercadoPagoSettings
{
    /// <summary>
    ///     Private Access Token (TEST-... or APP_USR-...). Required to create preferences.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    ///     Public Key (APP_USR-... / TEST-...). Optional for Checkout Pro redirect flow.
    /// </summary>
    public string PublicKey { get; set; } = string.Empty;

    /// <summary>
    ///     Optional webhook secret for signature validation.
    /// </summary>
    public string WebhookSecret { get; set; } = string.Empty;

    /// <summary>
    ///     Public API base URL used as Mercado Pago notification_url.
    ///     Example: https://frutilogic-platform-api-production.up.railway.app
    /// </summary>
    public string BackendPublicUrl { get; set; } = string.Empty;

    /// <summary>
    ///     Alias accepted from Railway if someone sets WebhookUrl instead of BackendPublicUrl.
    /// </summary>
    public string WebhookUrl { get; set; } = string.Empty;

    /// <summary>
    ///     Frontend base URL used for Checkout Pro back_urls.
    ///     Example: https://frutilogic-frontend.vercel.app
    /// </summary>
    public string FrontendPublicUrl { get; set; } = string.Empty;

    /// <summary>
    ///     When true, returns SandboxInitPoint instead of InitPoint (test credentials).
    /// </summary>
    public bool UseSandbox { get; set; } = true;

    /// <summary>
    ///     Alias for UseSandbox (some Railway setups use IsSandbox).
    /// </summary>
    public bool? IsSandbox { get; set; }

    public bool IsSandboxMode => IsSandbox ?? UseSandbox;

    public string ResolvedBackendPublicUrl =>
        !string.IsNullOrWhiteSpace(BackendPublicUrl) ? BackendPublicUrl : WebhookUrl;
}
