namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands
{
    /// <summary>
    /// Command to update the recommendations of the care guide.
    /// </summary>
    public record UpdateCareGuideCommand(string careGuideId, string title, string summary, double recommendedMinTemperature, double recommendedMaxTemperature, string recommendedPlaceStorage, string generalRecommendation);
}

