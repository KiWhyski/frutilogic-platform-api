using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
///     Query to get all product exits by product ID
/// </summary>
public record GetAllProductExitsByProductIdQuery(ObjectId ProductId);
