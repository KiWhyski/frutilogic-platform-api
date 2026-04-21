namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
///     Resource for updating the minimum stock of a product.
/// </summary>
public record UpdateProductMinimumStockResource(
        int NewMinimumStock
    );
