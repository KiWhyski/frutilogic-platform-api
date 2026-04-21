using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
///     Query to retrieve all the product exit history for a specific warehouse.
/// </summary>
public record GetAllProductExitsByWarehouseIdQuery(ObjectId WarehouseId);
