using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     Record class that serves as a Value Object for the name of a product.
/// </summary>
[BsonSerializer(typeof(ProductNameSerializer))]
public record ProductName()
{
    /// <summary>
    ///     The value of the product name.
    /// </summary>
    private string Value { get; } = string.Empty;

    /// <summary>
    ///     Default constructor for ProductName.
    /// </summary>
    /// <param name="name">
    ///     The name of the product.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided name is empty or whitespace.   
    /// </exception>
    public ProductName(string name) : this()
    {
        if (!IsNameValid(name))
            throw new ValueObjectValidationException(nameof(name), "Product name cannot be empty or whitespace.");
        
        Value = name;
    }
    
    /// <summary>
    ///     Static method to validate if the name is empty or whitespace.
    /// </summary>
    /// <param name="name">
    ///     The name to validate.
    /// </param>
    /// <returns>
    ///     True if the name is not empty or whitespace, false otherwise.  
    /// </returns>
    private static bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name.Trim());
    
    /// <summary>
    ///     Method to get the value of the product name.
    /// </summary>
    /// <returns>
    ///     The value of the product name. 
    /// </returns>
    public string GetValue() => Value;
}
