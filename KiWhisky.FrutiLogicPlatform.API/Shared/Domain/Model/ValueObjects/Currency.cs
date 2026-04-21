using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     The value object representing a currency.
/// </summary>
public record Currency()
{
    /// <summary>
    ///     The currency code (e.g., "USD", "EUR").
    /// </summary>
    private string Code { get; } = string.Empty;
    
    /// <summary>
    ///     Default constructor for the Currency value object.
    /// </summary>
    /// <param name="newCode">
    ///     The currency code as a string.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided currency code is not valid.
    /// </exception>
    public Currency(string newCode) : this()
    {
        try
        {
            Code = Enum.Parse<EValidCurrencyCodes>(newCode).ToString();
        }
        catch 
        {
            throw new ValueObjectValidationException(
                nameof(Currency), 
                "Tried to use an invalid currency code.");
        }
        
        Code = newCode.ToUpper();
    }

    /// <summary>
    ///     Method to get the currency code.
    /// </summary>
    /// <returns>
    ///     The currency code as a string.
    /// </returns>
    public string GetCode() => Code;
    
    /// <summary>
    ///     Method to compare the currency code with another code and check if they are equal.
    /// </summary>
    /// <param name="otherCode">
    ///     The other currency code to compare with.
    /// </param>
    /// <returns>
    ///     True if the codes are equal and valid; otherwise, false.
    /// </returns>
    public bool AreCodesEqual(string otherCode) => Enum.IsDefined(typeof(EValidCurrencyCodes), otherCode) && Code == otherCode;
}
