using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Command to decrease products from a warehouse.
/// </summary>
public record DecreaseProductsFromWarehouseCommand(ObjectId ProductId, ObjectId WarehouseId, ProductExpirationDate ExpirationDate, int QuantityToDecrease, EProductExitReasons ExitType);
