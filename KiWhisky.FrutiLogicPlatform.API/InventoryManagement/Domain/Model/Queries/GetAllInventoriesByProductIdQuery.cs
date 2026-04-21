using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
///     Query to get all inventories for a specific product.
/// </summary>
public record GetAllInventoriesByProductIdQuery(ObjectId ProductId);
