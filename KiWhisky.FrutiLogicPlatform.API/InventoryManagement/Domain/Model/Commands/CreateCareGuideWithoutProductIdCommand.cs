namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands
{
    public record CreateCareGuideWithoutProductIdCommand(string careGuideId, string accountId, string productAssociated, string title, string summary, double recommendedMinTemperature, double recommendedMaxTemperature, string recommendedPlaceStorage, string generalRecommendation);
}

