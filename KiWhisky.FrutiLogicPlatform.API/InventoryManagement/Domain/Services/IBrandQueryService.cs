using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;

/// <summary>
///     Interface for handling brand-related queries.
/// </summary>
public interface IBrandQueryService
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
    Task<IEnumerable<Brand>> Handle(GetAllBrandsQuery query);
}
