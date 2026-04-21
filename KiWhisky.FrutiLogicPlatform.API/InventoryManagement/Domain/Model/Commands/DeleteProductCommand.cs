using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Command to delete a product by its ID.
/// </summary>
public record DeleteProductCommand(ObjectId ProductId);
