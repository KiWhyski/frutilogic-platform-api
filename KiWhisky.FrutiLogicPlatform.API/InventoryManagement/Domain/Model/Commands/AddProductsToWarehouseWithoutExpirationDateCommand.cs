using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Method to add products to a warehouse without expiration date.
/// </summary>
public record AddProductsToWarehouseWithoutExpirationDateCommand(ObjectId ProductId, ObjectId WarehouseId, int QuantityToAdd);
