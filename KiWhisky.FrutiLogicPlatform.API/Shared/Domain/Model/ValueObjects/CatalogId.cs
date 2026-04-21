using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     The value object representing a catalog identifier.
/// </summary>
[BsonSerializer(typeof(CatalogIdSerializer))]
public record CatalogId()
{
    /// <summary>
    ///     The catalog identifier.
    /// </summary>
    private string Id { get; } = string.Empty;

    /// <summary>
    ///     The default constructor for the catalog identifier.
    /// </summary>
    /// <param name="id">
    ///     The catalog identifier.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided identifier is null or an empty string.
    /// </exception>
    public CatalogId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ValueObjectValidationException(nameof(AccountId), "Catalog ID must be a non-empty string.");
        }
        
        Id = id;
    }
    
    /// <summary>
    ///     The method to retrieve the catalog identifier.
    /// </summary>
    /// <returns>
    ///     The catalog identifier as a string.
    /// </returns>
    public string GetId() => Id;
}
