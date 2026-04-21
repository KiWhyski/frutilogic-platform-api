using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     This class represents a UserId Value Object.
///     It is used to encapsulate the identifier of a user in the system.
/// </summary>
[BsonSerializer(typeof(UserIdSerializer))]
public record OwnerUserId()
{
    /// <summary>
    /// This is the value of the UserId Value Object.
    /// </summary>
    private string Id { get; } = string.Empty;

    /// <summary>
    ///     Default constructor for the UserId Value Object.
    ///     It is used to validate if the provided id is a non-empty string.
    /// </summary>
    /// <param name="id"> The identifier for the user </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided id is null or an empty string.
    /// </exception>
    public OwnerUserId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ValueObjectValidationException(nameof(OwnerUserId), "User ID must be a non-empty string.");
        }
        
        Id = id;
    }
    
    /// <summary>
    ///     This method returns the string representation of the AccountId Value Object.
    /// </summary>
    public string GetId => Id;
}
