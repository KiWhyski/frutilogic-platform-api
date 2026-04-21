using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;

/// <summary>
///     Interface for handling product-type-related queries.
/// </summary>
public interface ITypeQueryService
{
    /// <summary>
    ///     Handle the retrieval of all product types.
    /// </summary>
    /// <param name="query">
    ///     The action query object.
    /// </param>
    /// <returns>
    ///     A list of product types.
    /// </returns>
    Task<IEnumerable<ProductType>> Handle(GetAllTypesQuery query);
}
