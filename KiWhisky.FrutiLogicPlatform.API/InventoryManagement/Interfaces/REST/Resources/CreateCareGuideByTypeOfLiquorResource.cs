using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources
{
    /// <summary>
    /// This record represents a resource for creating a care guide by type of liquor.
    /// </summary>
    public record CreateCareGuideByTypeOfLiquorResource(
        EProductTypes TypeOfLiquor,
        string ProductName,
        string Title,
        string Summary,
        double RecommendedMinTemperature,
        double RecommendedMaxTemperature
    );
}

