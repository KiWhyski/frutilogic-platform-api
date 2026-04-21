using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Command to add stock to a product inside a warehouse (inventory).
/// </summary>
public record AddProductsToWarehouseCommand(ObjectId ProductId, ObjectId WarehouseId, ProductExpirationDate ExpirationDate, int QuantityToAdd);
