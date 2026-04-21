namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
///     Resource for an inventory.
/// </summary>
public record InventoryResource(
    string InventoryId,
    string ProductId,
    string CurrentState,
    int Quantity,
    string WarehouseId,
    DateTime ExpirationDate,
    string AccountId
    );
