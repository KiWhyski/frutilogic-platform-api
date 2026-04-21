namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
///     Resource class for updating warehouse information.
/// </summary>
public record UpdateWarehouseInformationResource(
    string Name, 
    string AddressStreet,
    string AddressCity,
    string AddressDistrict,
    string AddressPostalCode,
    string AddressCountry,
    decimal TemperatureMin,
    decimal TemperatureMax,
    double Capacity,
    IFormFile? Image
    );
