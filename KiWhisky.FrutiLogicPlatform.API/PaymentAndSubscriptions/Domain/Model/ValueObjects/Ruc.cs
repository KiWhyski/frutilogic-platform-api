namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a RUC (Registro Único de Contribuyentes).
/// </summary>
public record Ruc
{
    /// <summary>
    ///     The RUC value, which must be exactly 11 digits.
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    ///     Default constructor for Ruc.
    /// </summary>
    /// <param name="value">
    ///     The RUC value, which must be exactly 11 digits.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     An exception is thrown if the value does not have exactly 11 digits or contains non-digit characters.
    /// </exception>
    public Ruc(string value)
    {
        if (value.Length != 11 || !value.All(char.IsDigit))
            throw new ArgumentOutOfRangeException(nameof(value), value, "Value must have exactly 11 digits.");
        
        Value = value;
    }
    
    /// <summary>
    ///     Default string representation of the RUC.
    /// </summary>
    /// <returns>
    ///     A string representing the RUC value.
    /// </returns>
    public override string ToString() => Value;
}
