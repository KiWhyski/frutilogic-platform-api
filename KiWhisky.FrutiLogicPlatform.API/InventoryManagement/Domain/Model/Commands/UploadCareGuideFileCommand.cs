namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands
{
    /// <summary>
    /// Command to upload/attach a file to an existing Care Guide.
    /// </summary>
    public record UploadCareGuideFileCommand(
        string careGuideId,
        string fileName,
        string contentType,
        byte[] data
    );
}

