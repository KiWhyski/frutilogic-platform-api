using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
///     Query to get an inventory by its ID.
/// </summary>
public record GetInventoryByIdQuery(ObjectId InventoryId);
