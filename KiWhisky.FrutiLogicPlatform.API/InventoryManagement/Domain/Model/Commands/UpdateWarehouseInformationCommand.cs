using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Record class that represents a command to update warehouse information.
/// </summary>
public record UpdateWarehouseInformationCommand(
    string WarehouseId,
    string Name, 
    WarehouseAddress NewAddress, 
    WarehouseTemperature NewTempLimits, 
    WarehouseCapacity TotalCapacity, 
    IFormFile? Image
    );
