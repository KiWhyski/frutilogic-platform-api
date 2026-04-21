using System.Text.RegularExpressions;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a business email.
/// </summary>
public record BusinessEmail
{
    /// <summary>
    ///     The regex pattern for validating email addresses.
    /// </summary>
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    /// <summary>
    ///     The business email value.
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    ///     Default constructor for BusinessEmail.
    /// </summary>
    /// <param name="value">
    ///     The business email value.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     An exception is thrown if the value is null, empty, or does not match the email format.
    /// </exception>
    public BusinessEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The email cannot be empty.", nameof(value));

        if (!EmailRegex.IsMatch(value))
            throw new ArgumentException("Invalid email format.", nameof(value));

        Value = value;
    }

    /// <summary>
    ///     Appropriate string representation of the business email.
    /// </summary>
    /// <returns>
    ///     A string representing the business email value.
    /// </returns>
    public override string ToString() => Value;
}
