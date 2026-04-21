using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources
{
    /// <summary>
    /// Form-data wrapper for uploading a Care Guide file.
    /// </summary>
    public class UploadCareGuideFileResource
    {
        [Required]
        public IFormFile File { get; set; }
    }
}

