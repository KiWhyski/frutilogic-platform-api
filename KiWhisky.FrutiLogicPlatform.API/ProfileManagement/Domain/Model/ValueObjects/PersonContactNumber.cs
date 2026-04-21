using System.Text.RegularExpressions;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;

/// <summary>
/// Value object representing a person's contact number.
/// </summary>
public record PersonContactNumber
{
    /// <summary>
    /// Gets the phone number.
    /// </summary>
    public string PhoneNumber { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonContactNumber"/> class.
    /// </summary>
    /// <param name="phoneNumber">The phone number.</param>
    /// <exception cref="ArgumentException">Thrown when phone number is null, whitespace, or invalid format.</exception>
    public PersonContactNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or whitespace.", nameof(phoneNumber));

        // Validate a phone number format (basic validation, can be enhanced)
        var cleanedNumber = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
        if (!Regex.IsMatch(cleanedNumber, @"^\+?[1-9]\d{1,14}$"))
            throw new ArgumentException("Phone number format is invalid.", nameof(phoneNumber));

        PhoneNumber = phoneNumber;
    }
}
