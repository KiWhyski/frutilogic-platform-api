namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands
{
    public record CreateCareGuideCommand(
        string careGuideId,
        string accountId,
        string productAssociated,
        string productId,
        string title,
        string summary,
        double recommendedMinTemperature,
        double recommendedMaxTemperature,
        string recommendedPlaceStorage,
        string generalRecommendation,
        string? imageUrl
    );
}

