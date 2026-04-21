namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
///     Resource for a warehouse.
/// </summary>
public record WarehouseResource(
        string WarehouseId,
        string Name,
        string AddressStreet,
        string AddressCity,
        string AddressDistrict,
        string AddressPostalCode,
        string AddressCountry,
        double Capacity,
        decimal TemperatureMin,
        decimal TemperatureMax,
        string ImageUrl
    );
