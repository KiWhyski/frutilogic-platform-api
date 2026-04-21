using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static assembler class to convert Product entity to ProductResource.
/// </summary>
public static class ProductResourceFromEntityAssembler
{
    /// <summary>
    ///     Static method to convert Product entity to ProductResource.
    /// </summary>
    /// <param name="entity">
    ///     The Product entity to convert.
    /// </param>
    /// <returns>
    ///     A ProductResource representation of the Product entity.
    /// </returns>
    public static ProductResource ToResourceFromEntity(Product entity)
    {
        return new ProductResource(
                entity.Id.ToString(),
                entity.Name,
                entity.Type.ToString(),
                entity.Brand,
                entity.UnitPrice.GetAmount(),
                entity.UnitPrice.GetCurrencyCode(),
                entity.MinimumStock.GetValue(),
                entity.TotalStockInStore,
                entity.Content.GetValue(),
                entity.ImageUrl.GetValue(),
                entity.AccountId.GetId,
                entity.SupplierId.GetId,
                entity.IsInWarehouse
            );
    }
}
