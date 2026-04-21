using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static class that provides methods for converting a <see cref="Brand"/> entity to a <see cref="BrandResource"/> resource.
/// </summary>
public static class BrandResourceFromEntityAssembler
{
    /// <summary>
    ///     Assemble a <see cref="BrandResource"/> from a <see cref="Brand"/> entity.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Brand"/> entity to convert.
    /// </param>
    /// <returns>
    ///     A new instance of <see cref="BrandResource"/> representing the provided entity.
    /// </returns>
    public static BrandResource ToResourceFromEntity(Brand entity)
    {
        return new BrandResource(entity.Name.GetDisplayName());
    }
}
