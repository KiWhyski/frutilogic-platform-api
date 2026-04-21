using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     The record that servers a Value Object for an identifier for the Product aggregate.
/// </summary>
[BsonSerializer(typeof(ProductIdSerializer))]
public record ProductId()
{
    /// <summary>
    ///     The value of the ProductId Value Object.
    /// </summary>
    private string Id { get; } = string.Empty;

    /// <summary>
    ///     The default constructor for the ProductId Value Object.
    /// </summary>
    /// <param name="id">
    ///     The identifier for the product.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided id is null or an empty string.
    /// </exception>
    public ProductId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ValueObjectValidationException(nameof(AccountId), "Product ID must be a non-empty string.");
        }
        
        Id = id;
    }
    
    /// <summary>
    ///     Method to return the string representation of the ProductId Value Object.
    /// </summary>
    public string GetId => Id;
}
