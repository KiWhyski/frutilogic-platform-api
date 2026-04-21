using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static assembler class to convert ProductType entity to TypeResource.
/// </summary>
public static class TypeResourceFromEntityAssembler
{
    /// <summary>
    ///     Static method to convert ProductType entity to TypeResource.
    /// </summary>
    /// <param name="entity">
    ///     The ProductType entity to convert.
    /// </param>
    /// <returns>
    ///     The TypeResource representation of the ProductType entity.
    /// </returns>
    public static TypeResource ToResourceFromEntity(ProductType entity)
    {
        return new TypeResource(entity.Name.GetDisplayName());
    }
}
