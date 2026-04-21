namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
///     Record for returning inventory information with the product related.
/// </summary>
public record InventoryWithProductResource(
        string InventoryId,
        string ProductId,
        string Name,
        string Type,
        string Brand,
        decimal UnitPrice,
        string MoneyCode,
        int MinimumStock,
        string ImageUrl,
        string CurrentState,
        int Quantity,
        string WarehouseId,
        string AccountId,
        DateOnly? ExpirationDate
    );
