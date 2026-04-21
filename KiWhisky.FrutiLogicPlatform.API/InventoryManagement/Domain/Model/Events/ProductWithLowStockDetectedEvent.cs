using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Events;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Events;

/// <summary>
///     Class that represents an event for a product with low stock.
/// </summary>
public class ProductWithLowStockDetectedEvent(
        string accountId, 
        string productId,
        string warehouseId,
        ProductExpirationDate? expirationDate
    ) : IDomainEvent
{
    /// <summary>
    ///     The identifier of the account which the product with low stock belongs to.
    /// </summary>
    public string AccountId { get; private set; } = accountId;
    
    /// <summary>
    ///     The identifier of the product with low stock.
    /// </summary>
    public string ProductId { get; private set; } = productId;
    
    /// <summary>
    ///     The identifier of the warehouse where the product with low stock is located.
    /// </summary>
    public string WarehouseId { get; private set; } = warehouseId;

    /// <summary>
    ///     The expiration date of the product inventory.
    /// </summary>
    public ProductExpirationDate? ExpirationDate { get; private set; } = expirationDate;
    
    /// <summary>
    ///     Date and time when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
