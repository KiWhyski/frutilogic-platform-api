using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Command for registering a new product in inventory.
/// </summary>
public record RegisterProductCommand(
        string Name,
        EProductTypes Type,
        string Brand,
        Money UnitPrice,
        ProductMinimumStock MinimumStock,
        ProductContent Content,
        IFormFile? Image,
        AccountId AccountId,
        AccountId SupplierId
    );
