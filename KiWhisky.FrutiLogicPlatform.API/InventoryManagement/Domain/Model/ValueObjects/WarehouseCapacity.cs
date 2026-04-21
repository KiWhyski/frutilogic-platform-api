using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///    Record class that serves as a Value Object for the capacity of a warehouse.
/// </summary>
[BsonSerializer(typeof(WarehouseCapacitySerializer))]
public record WarehouseCapacity()
{
    /// <summary>
    ///     The value of the capacity.
    /// </summary>
    private double Value { get; } = 0;

    /// <summary>
    ///     Default constructor for Capacity.
    /// </summary>
    /// <param name="totalCapacity">
    ///     The total capacity value of the warehouse. Must be a non-negative value and less than or equal to 100,000.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided total capacity value is negative or exceeds 100,000.
    /// </exception>
    public WarehouseCapacity(double totalCapacity) : this()
    {
        if (!IsCapacityValid(totalCapacity))
            throw new ValueObjectValidationException(nameof(WarehouseCapacity), $"{nameof(totalCapacity)} must be greater than {nameof(WarehouseCapacity)}");
        
        Value = totalCapacity;
    }

    /// <summary>
    ///     Method to validate the capacity value.
    /// </summary>
    /// <param name="totalCapacity">
    ///     The total capacity value of the warehouse to validate.
    /// </param>
    /// <returns>
    ///     A boolean indicating whether the capacity value is valid (non-negative and less than or equal to 100,000).
    /// </returns>
    private static bool IsCapacityValid(double totalCapacity) =>
        totalCapacity is >= 0 and <= 100000;
    
    /// <summary>
    ///     Method to retrieve the capacity value.
    /// </summary>
    /// <returns>
    ///     The decimal value representing the capacity of the warehouse.
    /// </returns>
    public double GetValue() => Value;
}
