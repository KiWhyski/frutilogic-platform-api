using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
///     Query to get all products by warehouse ID
/// </summary>
public record GetAllProductsByWarehouseIdQuery(ObjectId WarehouseId);
