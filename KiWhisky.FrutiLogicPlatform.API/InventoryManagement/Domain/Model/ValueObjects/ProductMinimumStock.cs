using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     The record class that serves as a Value Object for the minimum stock of a product.
/// </summary>
[BsonSerializer(typeof(ProductMinimumStockSerializer))]
public record ProductMinimumStock()
{
    /// <summary>
    ///     The value of the minimum stock.
    /// </summary>
    private int Value { get; } = 0;

    /// <summary>
    ///     Default constructor for the ProductMinimumStock Value Object.
    /// </summary>
    /// <param name="value">
    ///     The minimum stock of the product.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided value is not a positive integer.
    /// </exception>
    public ProductMinimumStock(int value) : this()
    {
        if (!IsMinimumStockValid(value)) 
            throw new ValueObjectValidationException(
                nameof(ProductMinimumStock), 
                "The minimum stock must be a positive integer.");

        Value = value;
    }
    
    /// <summary>
    ///     Static method to validate if the minimum stock is a positive integer.
    /// </summary>
    /// <param name="minimumStock">
    ///     The minimum stock to validate.
    /// </param>
    /// <returns>
    ///     True if the minimum stock is a positive integer, false otherwise.   
    /// </returns>
    private static bool IsMinimumStockValid(int minimumStock) => (minimumStock > 0);
    
    /// <summary>
    ///     Method to update the minimum stock.
    /// </summary>
    /// <param name="newMinimumStock">
    ///     The new minimum stock to set.
    /// </param>
    /// <returns>
    ///     The updated ProductMinimumStock Value Object.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided new minimum stock is not a positive integer.
    /// </exception>
    public ProductMinimumStock UpdateMinimumStock(int newMinimumStock) 
        => !IsMinimumStockValid(newMinimumStock)
            ? throw new ValueObjectValidationException(nameof(ProductMinimumStock), "The minimum stock to update must be a positive integer.") 
            : new ProductMinimumStock(newMinimumStock);
    
    /// <summary>
    ///     Method to get the value of the minimum stock.
    /// </summary>
    /// <returns>
    ///     The value of the minimum stock.  
    /// </returns>
    public int GetValue() => Value;
}
