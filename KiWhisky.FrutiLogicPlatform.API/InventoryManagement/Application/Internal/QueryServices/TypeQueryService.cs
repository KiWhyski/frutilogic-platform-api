using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Application.Internal.QueryServices;

/// <summary>
///     Service implementation for handling product-type-related queries.
/// </summary>
/// <param name="typeRepository">
///     The repository for handling the ProductTypes in the database.
/// </param>
public class TypeQueryService(
        ITypeRepository typeRepository
    ) : ITypeQueryService
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
    public async Task<IEnumerable<ProductType>> Handle(GetAllTypesQuery query)
    {
        return await typeRepository.GetAllAsync();
    }
}
