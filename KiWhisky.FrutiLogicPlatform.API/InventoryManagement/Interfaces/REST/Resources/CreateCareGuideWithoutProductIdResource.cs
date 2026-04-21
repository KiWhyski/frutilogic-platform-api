namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources
{
    /// <summary>
    /// This record represents a resource for the CreateCareGuideWithoutProductId command.
    /// </summary>
    public record CreateCareGuideWithoutProductIdResource(
        string AccountId,
        string Title,
        string Summary,
        double RecommendedMinTemperature,
        double RecommendedMaxTemperature,
        string RecommendedPlaceStorage,
        string GeneralRecommendation
    );
}

