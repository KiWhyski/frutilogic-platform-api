using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static assembler to convert UpdateProductMinimumStockResource to UpdateProductMinimumStockCommand.
/// </summary>
public static class UpdateProductMinimumStockCommandFromResourceAssembler
{
    /// <summary>
    ///     Static method to convert UpdateProductMinimumStockResource to UpdateProductMinimumStockCommand.
    /// </summary>
    /// <param name="id">
    ///     The product id as string.
    ///     It will be converted to ObjectId inside the method.
    /// </param>
    /// <param name="resource">
    ///     The UpdateProductMinimumStockResource to convert.
    /// </param>
    /// <returns>
    ///     A new instance of UpdateProductMinimumStockCommand.
    /// </returns>
    public static UpdateProductMinimumStockCommand ToCommandFromResource(string id,
        UpdateProductMinimumStockResource resource)
    {
        return new UpdateProductMinimumStockCommand(
                ObjectId.Parse(id),
                resource.NewMinimumStock
            );
    }
}
