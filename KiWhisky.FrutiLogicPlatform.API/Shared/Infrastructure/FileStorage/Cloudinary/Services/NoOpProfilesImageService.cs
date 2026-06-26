using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Application.Internal.OutBoundServices.FileStorage;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.FileStorage.Cloudinary.Services;

/// <summary>
/// Fallback when Cloudinary is not configured (e.g. Railway without image storage).
/// </summary>
public class NoOpProfilesImageService : IProfilesImageService
{
    private const string DefaultProfileUrl =
        "https://res.cloudinary.com/deuy1pr9e/image/upload/v1759710739/Default-profile_xbpv55.jpg";

    public string UploadImage(IFormFile file) => DefaultProfileUrl;

    public bool DeleteImage(string imageUrl) => false;
}
