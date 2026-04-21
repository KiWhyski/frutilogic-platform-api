using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     Represents the expiration date of a product in inventory.
/// </summary>
[BsonSerializer(typeof(ProductExpirationDateSerializer))]
public record ProductExpirationDate()
{
    /// <summary>
    ///     The value of the expiration date.
    /// </summary>
    private DateOnly Value { get; } = new DateOnly();

    /// <summary>
    ///     Default constructor for the ProductExpirationDate Value Object.
    /// </summary>
    /// <param name="expirationDate">
    ///     The expiration date.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided expiration date is in the past.   
    /// </exception>
    public ProductExpirationDate(DateOnly? expirationDate) : this()
    {
        if (expirationDate != null) 
            Value = expirationDate.Value;
    }
    
    /// <summary>
    ///     Retrieves the underlying expiration date value for the ProductExpirationDate Value Object.
    /// </summary>
    /// <returns>
    ///     The expiration date.
    /// </returns>
    public DateOnly GetValue() => Value;
    
    /// <summary>
    ///     Method to convert the ProductExpirationDate Value Object to a string.
    /// </summary>
    /// <returns>
    ///     A string representing the expiration date. 
    /// </returns>
    public override string ToString() => Value.ToString("yyyy-MM-dd");
}
