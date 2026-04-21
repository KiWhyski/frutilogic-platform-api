using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
/// Query to get a care guide by its type of liquor and account ID.
/// </summary>
public record GetCareGuideByTypeOfLiquorQuery(
    string AccountId,
    EProductTypes TypeOfLiquor
);

