using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static assembler class to convert RegisterWarehouseResource to RegisterWarehouseCommand.
/// </summary>
public static class RegisterWarehouseCommandFromResourceAssembler
{
    /// <summary>
    ///     Static method to convert RegisterWarehouseResource to RegisterWarehouseCommand.  
    /// </summary>
    /// <param name="resource">
    ///     The RegisterWarehouseResource to convert.
    /// </param>
    /// <param name="AccountId">
    ///     The account id of the user registering the warehouse.
    /// </param>
    /// <returns>
    ///     A RegisterWarehouseCommand representation of the RegisterWarehouseResource.
    /// </returns>
    public static RegisterWarehouseCommand ToCommandFromResource(RegisterWarehouseResource resource, string AccountId)
    {
        // Create the address object
        var address = new WarehouseAddress(
            resource.AddressStreet, 
            resource.AddressCity, 
            resource.AddressDistrict, 
            resource.AddressPostalCode, 
            resource.AddressCountry);
        
        // Create the temperature limits object
        var tempLimits = new WarehouseTemperature(resource.TemperatureMin, resource.TemperatureMax);
        
        // Create the capacity object
        var capacity = new WarehouseCapacity(resource.Capacity);
        
        // Create the account id object
        var accountId = new AccountId(AccountId);
        
        // Return the command
        return new RegisterWarehouseCommand(
                resource.Name,
                address,
                tempLimits,
                capacity,
                resource.Image,
                accountId
            );
    }
}
