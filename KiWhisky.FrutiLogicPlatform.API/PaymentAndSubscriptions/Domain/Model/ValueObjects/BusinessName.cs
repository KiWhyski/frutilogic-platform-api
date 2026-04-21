namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a business name.
/// </summary>
public record BusinessName
{
    /// <summary>
    ///     The business name value.
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    ///     Default constructor for ORM and serialization purposes.
    /// </summary>
    /// <param name="value">
    ///     The business name value.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     An exception is thrown if the value is null, empty, or exceeds 200 characters.
    /// </exception>
    public BusinessName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The business name cannot be null or empty.", nameof(value));

        if (value.Length > 200)
            throw new ArgumentException("The business name cannot exceed 200 characters.", nameof(value));

        Value = value;
    }

    /// <summary>
    ///     Method to return the string representation of the business name.
    /// </summary>
    /// <returns>
    ///     A string representing the business name.
    /// </returns>
    public override string ToString() => Value;
}
