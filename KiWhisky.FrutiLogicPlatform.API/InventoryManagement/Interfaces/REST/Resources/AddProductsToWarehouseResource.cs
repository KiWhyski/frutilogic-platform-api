using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
///     Resource class for adding products to a warehouse.
/// </summary>
public record AddProductsToWarehouseResource(
    int QuantityToAdd,
    DateTime? ExpirationDate
);
