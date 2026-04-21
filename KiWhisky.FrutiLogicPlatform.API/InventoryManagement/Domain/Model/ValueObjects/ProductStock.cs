using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     The record class that serves as a Value Object for the stock of a product.
/// </summary>
[BsonSerializer(typeof(ProductStockSerializer))]
public record ProductStock()
{
    /// <summary>
    ///     The value of the stock.
    /// </summary>
    private int Value { get; } = 0;

    /// <summary>
    ///     Default constructor for the ProductStock Value Object.
    /// </summary>
    /// <param name="stock">
    ///     The initial stock of the product.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided stock is not a non-negative integer.
    /// </exception>
    public ProductStock(int stock) : this()
    {
        if (!IsStockValid(stock)) throw new ValueObjectValidationException(nameof(ProductStock), "Stock must be a non-negative integer.");
        
        Value = stock;
    }
    
    /// <summary>
    ///     Method to validate if the stock is a non-negative integer.
    /// </summary>
    /// <param name="stock">
    ///     The stock to validate.
    /// </param>
    /// <returns>
    ///     True if the stock is a non-negative integer, false otherwise. 
    /// </returns>
    private static bool IsStockValid(int stock) => (stock >= 0);
    
    /// <summary>
    ///     Method to add stock to the current stock.
    /// </summary>
    /// <param name="stockToAdd">
    ///     The stock to add.
    /// </param>
    /// <returns>
    ///     A new ProductStock Value Object with the updated stock.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided stock to add is not a non-negative integer.
    /// </exception>
    public ProductStock AddStock(int stockToAdd) 
        => !IsStockValid(stockToAdd) 
            ? throw new ValueObjectValidationException(nameof(ProductStock), "Stock to add must be a non-negative integer.") 
            : new ProductStock(Value + stockToAdd);
    
    /// <summary>
    ///     Method to decrease stock from the current stock.
    /// </summary>
    /// <param name="stockToDecrease">
    ///     The stock to decrease.
    /// </param>
    /// <returns>
    ///     A new ProductStock Value Object with the updated stock.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided stock to decrease is not a non-negative integer and less than or equal to the current stock.
    /// </exception>
    public ProductStock DecreaseStock(int stockToDecrease)
        => (!IsStockValid(stockToDecrease) && stockToDecrease > Value)
            ? throw new ValueObjectValidationException(nameof(ProductStock), "Stock to decrease must be a non-negative integer and less than or equal to the current stock.")
            : new ProductStock(Value - stockToDecrease);
    
    /// <summary>
    ///     Method to get the value of the stock.   
    /// </summary>
    public int GetValue => Value;
}
