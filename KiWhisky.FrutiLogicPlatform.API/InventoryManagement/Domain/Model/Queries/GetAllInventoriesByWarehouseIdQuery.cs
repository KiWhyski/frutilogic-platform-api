using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
///     Query to get all inventories (products) for a specific warehouse.
/// </summary>
public record GetAllInventoriesByWarehouseIdQuery(ObjectId WarehouseId);
