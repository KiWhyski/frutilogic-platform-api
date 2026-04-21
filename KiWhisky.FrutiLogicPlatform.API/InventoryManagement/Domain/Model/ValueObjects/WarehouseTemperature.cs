using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     Record class that serves as a Value Object for the temperature range of a warehouse.
/// </summary>
[BsonSerializer(typeof(WarehouseTemperatureSerializer))]
public record WarehouseTemperature()
{
    /// <summary>
    ///     The maximum temperature value of the warehouse.
    /// </summary>
    private decimal MinTemperature { get; } = 0;
    
    /// <summary>
    ///     The minimum temperature value of the warehouse.
    /// </summary>
    private decimal MaxTemperature { get; } = 0;

    /// <summary>
    ///     Default constructor for Temperature.
    /// </summary>
    /// <param name="minTemperature">
    ///     The minimum temperature value of the warehouse. Must be less than the maximum temperature.
    /// </param>
    /// <param name="maxTemperature">
    ///     The maximum temperature value of the warehouse. Must be greater than the minimum temperature and less than or equal to 50.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided temperature values are invalid.
    /// </exception>
    public WarehouseTemperature(decimal minTemperature, decimal maxTemperature) : this()
    {
        if (!IsTemperatureValid(maxTemperature, minTemperature))
            throw new ValueObjectValidationException(nameof(WarehouseTemperature), $"{nameof(maxTemperature)} must be greater than {nameof(minTemperature)} and less than or equal to 50");
        
        MinTemperature = minTemperature;
        MaxTemperature = maxTemperature;
    }

    /// <summary>
    ///     Default constructor for Temperature.
    /// </summary>
    /// <param name="maxTemperature">
    ///     Indicates the maximum temperature value of the warehouse. Must be greater than the minimum temperature and less than or equal to 50.
    /// </param>
    /// <param name="minTemperature">
    ///     Indicates the minimum temperature value of the warehouse. Must be less than the maximum temperature.
    /// </param>
    /// <returns>
    ///     A boolean indicating whether the temperature values are valid.
    /// </returns>
    private static bool IsTemperatureValid(decimal maxTemperature, decimal minTemperature) =>
        (maxTemperature > minTemperature) && (maxTemperature <= 50);
    
    /// <summary>
    ///     The method to retrieve the minimum temperature value of the warehouse.
    /// </summary>
    /// <returns>
    ///     The minimum temperature value of the warehouse.
    /// </returns>
    public decimal GetMinTemperature() => MinTemperature;
    
    /// <summary>
    ///     Method to retrieve the maximum temperature value of the warehouse.
    /// </summary>
    /// <returns>
    ///     The maximum temperature value of the warehouse.
    /// </returns>
    public decimal GetMaxTemperature() => MaxTemperature;
}
