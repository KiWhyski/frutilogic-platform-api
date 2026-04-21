using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     This record class serves as a Value Object for an identifier for the Account aggregate.
/// </summary>
[BsonSerializer(typeof(AccountIdSerializer))]
public record AccountId
{
    /// <summary>
    /// This is the value of the AccountId Value Object.
    /// </summary>
    private string Id { get; }

    /// <summary>
    ///     Private constructor to prevent direct instantiation without validation
    /// </summary>
    public AccountId(string id)
    {
        Id = id;
    }

    /// <summary>
    ///     Factory method to create a new AccountId
    /// </summary>
    /// <param name="id">The identifier for the account</param>
    /// <returns>A new AccountId instance</returns>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided id is null, empty, or whitespace.
    /// </exception>
    public static AccountId Create(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ValueObjectValidationException(nameof(AccountId), "Account ID must be a non-empty string.");
        }
        
        return new AccountId(id.Trim());
    }

    /// <summary>
    ///     Creates a new AccountId with a generated GUID
    /// </summary>
    public static AccountId CreateNew()
    {
        return new AccountId(Guid.NewGuid().ToString());
    }
    
    /// <summary>
    ///     This method returns the string representation of the AccountId Value Object.
    /// </summary>
    public string GetId => Id;
}
