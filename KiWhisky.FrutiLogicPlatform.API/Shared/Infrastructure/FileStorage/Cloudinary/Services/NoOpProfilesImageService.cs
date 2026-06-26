using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Application.Internal.OutBoundServices.FileStorage;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.FileStorage;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.FileStorage.Cloudinary.Services;

/// <summary>
/// Image uploads disabled — auth and profiles work without external storage.
/// </summary>
public class NoOpProfilesImageService : IProfilesImageService
{
    public string UploadImage(IFormFile file) => DefaultImageUrls.ProfilePicture;

    public bool DeleteImage(string imageUrl) => false;
}
