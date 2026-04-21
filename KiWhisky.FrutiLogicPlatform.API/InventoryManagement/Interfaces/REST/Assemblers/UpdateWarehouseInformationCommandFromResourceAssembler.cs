using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static assembler class to convert UpdateWarehouseInformationResource to UpdateWarehouseInformationCommand.
/// </summary>
public static class UpdateWarehouseInformationCommandFromResourceAssembler
{
    /// <summary>
    ///     Static method to convert UpdateWarehouseInformationResource to UpdateWarehouseInformationCommand. 
    /// </summary>
    /// <param name="warehouseId">
    ///     The id of the warehouse to update as string.
    /// </param>
    /// <param name="resource">
    ///     The UpdateWarehouseInformationResource to convert.
    /// </param>
    /// <returns>
    ///     An UpdateWarehouseInformationCommand representation of the UpdateWarehouseInformationResource.
    /// </returns>
    public static UpdateWarehouseInformationCommand ToCommandFromResource(string warehouseId, UpdateWarehouseInformationResource resource)
    {
        // Create the updated address object
        var address = new WarehouseAddress(
            resource.AddressStreet, 
            resource.AddressCity, 
            resource.AddressDistrict, 
            resource.AddressPostalCode, 
            resource.AddressCountry);
        
        // Create the updated temperature limits object
        var tempLimits = new WarehouseTemperature(resource.TemperatureMin, resource.TemperatureMax);
        
        // Create the updated capacity object
        var capacity = new WarehouseCapacity(resource.Capacity);
        
        // Returns the command
        return new UpdateWarehouseInformationCommand(
            warehouseId,
            resource.Name,
            address,
            tempLimits,
            capacity,
            resource.Image
        );
    }
}
