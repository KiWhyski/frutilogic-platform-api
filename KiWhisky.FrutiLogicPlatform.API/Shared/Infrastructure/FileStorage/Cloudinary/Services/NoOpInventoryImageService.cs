using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Application.Internal.OutboundServices.FileStorage;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.FileStorage;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.FileStorage.Cloudinary.Services;

/// <summary>
/// Fallback when Cloudinary is not configured.
/// </summary>
public class NoOpInventoryImageService : IInventoryImageService
{
    public string UploadImage(IFormFile file) => DefaultImageUrls.ProfilePicture;

    public bool DeleteImage(string imageUrl) => false;
}
