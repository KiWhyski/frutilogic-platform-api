using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Record representing the command to register a new warehouse.
/// </summary>
public record RegisterWarehouseCommand(
        string Name,
        WarehouseAddress Address,
        WarehouseTemperature Temperature,
        WarehouseCapacity Capacity,
        IFormFile? Image,
        AccountId AccountId
    );
