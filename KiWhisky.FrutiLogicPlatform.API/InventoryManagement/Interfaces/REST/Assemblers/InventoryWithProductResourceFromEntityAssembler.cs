using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Extensions;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static class that provides methods for converting an Inventory entity and its related Product entity into a combined resource.
/// </summary>
public static class InventoryWithProductResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts an Inventory entity and its related Product entity into a combined resource.
    /// </summary>
    /// <param name="inventory">
    ///     The inventory entity.
    /// </param>
    /// <param name="product">
    ///     The product entity related to the inventory.
    /// </param>
    /// <returns>
    ///     A resource combining both Inventory and Product information.
    /// </returns>
    public static InventoryWithProductResource ToResourceFromEntity(Inventory inventory, Product product)
    {
        return new InventoryWithProductResource
        (
            inventory.Id.ToString(),
            inventory.ProductId.ToString(),
            product.Name,
            product.Type.ToString(),
            product.Brand,
            product.UnitPrice.GetAmount(),
            product.UnitPrice.GetCurrencyCode(),
            product.MinimumStock.GetValue(),
            product.ImageUrl.GetValue(),
            inventory.CurrentState.GetDisplayName(),
            inventory.Quantity.GetValue,
            inventory.WarehouseId.ToString(),
            product.AccountId.GetId,
            inventory.ExpirationDate?.GetValue()
        );
    }
}
