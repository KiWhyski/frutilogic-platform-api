namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources
{
    /// <summary>
    /// This record defines the care guide resource.
    /// </summary>
    public record CareGuideResource(
        string CareGuideId,
        string AccountId,
        string ProductId,
        string Title,
        string Summary,
        double RecommendedMinTemperature,
        double RecommendedMaxTemperature,
        string RecommendedPlaceStorage,
        string GeneralRecommendation,
        string? TypeOfLiquor,
        string? ProductName,
        string? ImageUrl
    );
}

