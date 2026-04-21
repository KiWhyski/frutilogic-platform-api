using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;

/// <summary>
///     This interface defines the contract for a repository that manages Warehouse aggregates.
/// </summary>v
public interface IWarehouseRepository : IBaseRepository<Warehouse>
{
    /// <summary>
    ///     This method retrieves all the warehouses associated with a specific account ID.
    /// </summary>
    /// <returns>
    ///     A list of warehouse objects
    /// </returns>
    Task<ICollection<Warehouse>> FindByAccountIdAsync(AccountId accountId);
    
    /// <summary>
    ///     This method checks if a warehouse with the specified name and profile ID exists in the database.
    /// </summary>
    /// <returns>
    ///     True if a warehouse exists with the specified name and profile ID; otherwise, false.
    /// </returns>
    Task<bool> ExistByNameIgnoreCaseAndAccountIdAsync(string name, AccountId accountId);
    
    /// <summary>
    ///     This method checks if a warehouse with the specified name, profile ID, and a different warehouse ID exists in the database.
    /// </summary>
    /// <returns>
    ///     True if a warehouse exists with the specified name, profile ID, and a different warehouse ID; otherwise, false.
    /// </returns>
    Task<bool> ExistsByNameIgnoreCaseAndAccountIdAndWarehouseIdIsNotAsync(string name, AccountId accountId, string warehouseId);
    
    /// <summary>
    ///     This method checks if a warehouse exists by its street, city, postal code, and account ID.
    /// </summary>
    /// <returns>
    ///     True if a warehouse exists with the exact address components and profile ID; otherwise, false.
    /// </returns>
    Task<bool> ExistsByStreetAndCityAndPostalCodeIgnoreCaseAndAccountIdAsync(string street, string city, string postalCode, AccountId accountId);
    
    /// <summary>
    ///     This method checks if a warehouse exists by its address, city, postal code, profile ID, and a different warehouse ID.
    /// </summary>
    /// <returns>
    ///     Exists by address, city, postal code, profile ID, and a different warehouse ID; otherwise, false.
    /// </returns>
    Task<bool> ExistsByStreetAndCityAndPostalCodeIgnoreCaseAndAccountIdAndWarehouseIdIsNotAsync(string street, string city, string postalCode, AccountId accountId, string warehouseId);
    
    /// <summary>
    ///     Get the account ID associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">
    ///     The unique identifier of the warehouse.
    /// </param>
    /// <returns>
    ///     The account ID associated with the warehouse.
    /// </returns>
    Task<string> FindAccountIdByWarehouseIdAsync(string warehouseId);
    
    /// <summary>
    ///     Get the image URL associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">
    ///     The unique identifier of the warehouse.
    /// </param>
    /// <returns>
    ///     The image URL associated with the warehouse.
    /// </returns>
    Task<string> FindImageUrlByWarehouseIdAsync(string warehouseId);
    
    /// <summary>
    ///     This method counts the number of warehouses associated with a specific account ID.
    /// </summary>
    /// <param name="accountId">
    ///     The unique identifier of the account.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation, containing the count of warehouses.
    /// </returns>
    Task<int> CountByAccountIdAsync(AccountId accountId);
}
