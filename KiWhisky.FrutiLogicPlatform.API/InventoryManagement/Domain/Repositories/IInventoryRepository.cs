using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;
using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;

/// <summary>
///     Interface for managing Inventory entities.
/// </summary>
public interface IInventoryRepository : IBaseRepository<Inventory>
{
    /// <summary>
    ///     Method to get an inventory item by product ID, warehouse ID and expiration date.
    /// </summary>
    /// <param name="productId">
    ///     The ID of the product to find inventory items for.
    /// </param>
    /// <param name="warehouseId">
    ///     The ID of the warehouse to find inventory items for.
    /// </param>
    /// <param name="expirationDate">
    ///     The expiration date of the inventory item to find.
    /// </param>
    /// <returns>
    ///     An inventory item if found; otherwise, null.
    /// </returns>
    Task<Inventory?> GetByProductIdWarehouseIdAndExpirationDateAsync(ObjectId productId, ObjectId warehouseId, ProductExpirationDate expirationDate);
    
    /// <summary>
    ///     Method to get an inventory item by product ID and warehouse ID.
    ///     Used when the inventory does not have an expiration date.
    /// </summary>
    /// <param name="productId">
    ///     The ID of the product to find inventory items for.
    /// </param>
    /// <param name="warehouseId">
    ///     The ID of the warehouse to find inventory items for.
    /// </param>
    /// <returns>
    ///     An inventory item if found; otherwise, null.
    /// </returns>
    Task<Inventory?> GetByProductIdWarehouseIdAsync(ObjectId productId, ObjectId warehouseId);
    
    /// <summary>
    ///     Method to check if an inventory item exists by product ID, warehouse ID and expiration date.
    /// </summary>
    /// <param name="productId">
    ///     The ID of the product to find inventory items for.
    /// </param>
    /// <param name="warehouseId">
    ///     The ID of the warehouse to find inventory items for.
    /// </param>
    /// <param name="expirationDate">
    ///     The expiration date of the inventory item to find.
    /// </param>
    /// <returns>
    ///     A boolean indicating whether an inventory item exists.
    /// </returns>
    Task<bool> ExistsByProductIdWarehouseIdAndExpirationDateAsync(ObjectId productId, ObjectId warehouseId, ProductExpirationDate expirationDate);
    
    /// <summary>
    ///     Method to check if an inventory item exists by product ID, warehouse ID and without expiration date.
    /// </summary>
    /// <param name="productId">
    ///     The ID of the product to find inventory items for.
    /// </param>
    /// <param name="warehouseId">
    ///     The ID of the warehouse to find inventory items for.
    /// </param>
    /// <returns>
    ///     A boolean indicating whether an inventory item exists.
    /// </returns>
    Task<bool> ExistsByProductIdWarehouseIdAsync(ObjectId productId, ObjectId warehouseId);
    
    /// <summary>
    ///     Method to check if an inventory item has an expiration date.
    /// </summary>
    /// <param name="productId">
    ///     The ID of the product to find inventory items for.
    /// </param>
    /// <param name="warehouseId">
    ///     The ID of the warehouse to find inventory items for.
    /// </param>
    /// <returns>
    ///     A boolean indicating whether an inventory item has an expiration date.
    /// </returns>
    Task<bool> HasExpirationDateAsync(ObjectId productId, ObjectId warehouseId);
    
    /// <summary>
    ///     Method to find all inventory items by product ID.
    /// </summary>
    /// <param name="productId">
    ///     The ID of the product to find inventory items for. 
    /// </param>
    /// <returns>
    ///     A list of inventory items for the specified product.
    /// </returns>
    Task<IEnumerable<Inventory>> FindByProductIdAsync(ObjectId productId);
    
    /// <summary>
    ///     Method to find all inventory items by warehouse ID.   
    /// </summary>
    /// <param name="warehouseId">
    ///     The ID of the warehouse to find inventory items for.
    /// </param>
    /// <returns>
    ///     A list of inventory items for the specified warehouse.
    /// </returns>
    Task<IEnumerable<Inventory>> FindByWarehouseIdAsync(ObjectId warehouseId);
}
