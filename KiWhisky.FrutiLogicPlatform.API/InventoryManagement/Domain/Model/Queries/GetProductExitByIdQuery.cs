using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
///     Query to get a product exit by its ID.
/// </summary>
public record GetProductExitByIdQuery(ObjectId ProductExitId);
