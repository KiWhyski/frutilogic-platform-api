using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     This record class serves as a Value Object for an email address.
/// </summary>
[BsonSerializer(typeof(EmailSerializer))]
public partial record Email()
{
    /// <summary>
    ///     This is the value of the Email Value Object.
    /// </summary>
    public string Value { get; init; } = string.Empty;
    
    /// <summary>
    ///     Default constructor for the Email Value Object.
    ///     It is used to validate if the provided value is a non-empty string and follows a basic email format.
    /// </summary>
    /// <param name="value">The value pf the email</param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided value is null, empty, or does not match a basic email format.
    /// </exception>
    public Email(string? value) : this()
    {
        if (value == null || value.Trim().Length == 0)
        {
            throw new ValueObjectValidationException(nameof(Email), "Email must be a non-empty string.");
        }

        // Simple email format validation
        if (!MyRegex().IsMatch(value))
        {
            throw new ValueObjectValidationException(nameof(Email), "The email does not comply with the structure of a conventional email.");
        }

        Value = value;
    }

    public override string ToString() => Value;

    /// <summary>
    ///     Defines a regex for basic email validation.
    /// </summary>
    /// <returns>
    ///     Regex that matches a basic email format.
    /// </returns>
    [System.Text.RegularExpressions.GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
    
    /// <summary>
    ///     Method to get the value of the Email Value Object.
    /// </summary>
    public string GetValue => Value;
}
