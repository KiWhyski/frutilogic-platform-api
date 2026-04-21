using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     This record class serves as a Value Object for an identifier for the Inventory aggregate.
/// </summary>
[BsonSerializer(typeof(InventoryIdSerializer))]
public record InventoryId()
{
    /// <summary>
    ///     The value of the InventoryId Value Object.
    /// </summary>
    private string Id { get; } = string.Empty;
    
    /// <summary>
    ///     Default constructor for the InventoryId Value Object.
    ///     It is used to validate if the provided id is a non-empty string.
    /// </summary>
    /// <param name="id">
    ///     The identifier for the inventory.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided id is null or an empty string.
    /// </exception>
    public InventoryId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ValueObjectValidationException(nameof(InventoryId), "Inventory ID must be a non-empty string.");
        }
        
        Id = id;
    }
    
    /// <summary>
    ///     This method returns the string representation of the InventoryId Value Object.
    /// </summary>
    public string GetId => Id;
}
