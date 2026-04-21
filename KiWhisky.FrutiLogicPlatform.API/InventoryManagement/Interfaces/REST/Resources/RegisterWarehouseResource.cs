namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
///     Resource class for registering a new warehouse.
/// </summary>
public record RegisterWarehouseResource(
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
