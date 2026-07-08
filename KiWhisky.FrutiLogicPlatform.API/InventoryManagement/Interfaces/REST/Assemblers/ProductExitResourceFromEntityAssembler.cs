using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Extensions;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static assembler to convert ProductExit entity to ProductExitResource.
/// </summary>
public static class ProductExitResourceFromEntityAssembler
{
    /// <summary>
    ///     Static method to convert ProductExit entity to ProductExitResource.
    /// </summary>
    /// <param name="entity">
    ///     The ProductExit entity to convert.
    /// </param>
    /// <returns>
    ///     A new instance of ProductExitResource.
    /// </returns>
    public static ProductExitResource ToResourceFromEntity(ProductExit entity)
    {
        return new ProductExitResource(
            entity.ProductId,
            entity.ProductName,
            entity.WarehouseId,
            entity.ExpirationDate,
            entity.WarehouseName,
            entity.ExitType.GetDisplayName(),
            entity.OutputQuantity,
            entity.PreviousQuantity,
            entity.CreatedAt
        );
    }
}
