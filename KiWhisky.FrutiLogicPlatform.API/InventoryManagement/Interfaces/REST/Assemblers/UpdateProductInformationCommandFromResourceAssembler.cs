using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static assembler to convert UpdateProductInformationResource to UpdateProductInformationCommand.
/// </summary>
public static class UpdateProductInformationCommandFromResourceAssembler
{
    /// <summary>
    ///     Static method to convert UpdateProductInformationResource to UpdateProductInformationCommand.
    /// </summary>
    /// <param name="id">
    ///     The id of the product to update.
    ///     This comes from the route parameter.
    /// </param>
    /// <param name="resource">
    ///     The resource to convert.
    /// </param>
    /// <returns>
    ///     A new instance of UpdateProductInformationCommand.
    /// </returns>
    public static UpdateProductInformationCommand ToCommandFromResource(string id, UpdateProductInformationResource resource)
    {
        return new UpdateProductInformationCommand(
                ObjectId.Parse(id),
                resource.Name,
                new Money(resource.UnitPrice, new Currency(resource.Code)),
                new ProductMinimumStock(resource.MinimumStock),
                resource.Image
            );
    }
}
