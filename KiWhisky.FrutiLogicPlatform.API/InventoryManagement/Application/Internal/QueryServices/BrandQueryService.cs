using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Application.Internal.QueryServices;

/// <summary>
///     Class that implements the <see cref="IBrandQueryService"/> interface.
/// </summary>
/// <param name="brandRepository">
///     The repository for handling the Brands in the database.
/// </param>
public class BrandQueryService(
        IBrandRepository brandRepository
    ) : IBrandQueryService
{
    /// <summary>
    ///     Method to handle the retrieval of all brands.
    /// </summary>
    /// <param name="query">
    ///     The query object containing parameters for retrieving all brands.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an enumerable of Brand entities.
    /// </returns>
    public async Task<IEnumerable<Brand>> Handle(GetAllBrandsQuery query)
    {
        return await brandRepository.GetAllAsync();
    }
}
