namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.FileStorage.Cloudinary.Configuration;

public static class CloudinarySettingsExtensions
{
    public static bool IsConfigured(this CloudinarySettings? settings) =>
        settings is not null
        && !string.IsNullOrWhiteSpace(settings.CloudName)
        && !string.IsNullOrWhiteSpace(settings.ApiKey)
        && !string.IsNullOrWhiteSpace(settings.ApiSecret);
}
