namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources
{
    /// <summary>
    /// This record defines the resource for UpdateCareGuideCommand
    /// </summary>
    public record UpdateCareGuideResource(
        string CareGuideId,
        string Title,
        string Summary,
        double RecommendedMinTemperature,
        double RecommendedMaxTemperature,
        string RecommendedPlaceStorage,
        string GeneralRecommendation
    );
}

