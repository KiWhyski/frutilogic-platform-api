using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     The Money value object represents a monetary amount with its associated currency.
/// </summary>
[BsonSerializer(typeof(MoneySerializer))]
public record Money()
{
    /// <summary>
    ///     The amount of money.
    /// </summary>
    private decimal Amount { get; } = (decimal)0.0;

    /// <summary>
    ///     The currency of the money.
    /// </summary>
    private Currency Currency { get; } = new Currency(nameof(EValidCurrencyCodes.USD));

    /// <summary>
    ///     Initializes a new instance of the <see cref="Money" /> class with the specified amount and currency.
    /// </summary>
    /// <param name="amount">The amount of money.</param>
    /// <param name="currency">The currency of the money.</param>
    public Money(decimal amount, Currency currency) : this()
    {
        if (amount < 0)
        {
            throw new ValueObjectValidationException(nameof(Money), "Amount must be zero or greater.");
        }

        Amount = amount;
        Currency = currency ?? throw new ValueObjectValidationException(nameof(Money), "Currency cannot be null.");
    }
    
    /// <summary>
    ///     Method to get the amount of money.
    /// </summary>
    /// <returns>
    ///     The amount of money as a decimal.
    /// </returns>
    public decimal GetAmount() => Amount;
    
    /// <summary>
    ///     Method to get the currency code of the money.
    /// </summary>
    /// <returns>
    ///     The currency code as a string.
    /// </returns>
    public string GetCurrencyCode() => Currency.GetCode();

    /// <summary>
    ///     Method to add two Money objects together.
    /// </summary>
    /// <param name="otherMoney">
    ///     The other Money object to add.
    /// </param>
    /// <returns>
    ///     A new Money object representing the sum of the two Money objects.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown if the currency codes of the two Money objects are not equal.
    /// </exception>
    public Money Add(Money otherMoney)
    {
        return !Currency.AreCodesEqual(otherMoney.GetCurrencyCode()) 
            ? throw new ValueObjectValidationException(
                nameof(Money), 
                "Currency code of other money has to be equal to currency code.") 
            : new Money(Amount + otherMoney.Amount, Currency);
    }

    /// <summary>
    ///     Method to subtract one Money object from another.
    /// </summary>
    /// <param name="otherMoney">
    ///     The other Money object to subtract.
    /// </param>
    /// <returns>
    ///     The result of the subtraction as a new Money object.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown if the currency codes of the two Money objects are not equal or if the amount to subtract is greater than the current amount.
    /// </exception>
    public Money Subtract(Money otherMoney)
    {
        return !Currency.AreCodesEqual(otherMoney.GetCurrencyCode()) && otherMoney.Amount <= Amount
            ? throw new ValueObjectValidationException(
                nameof(Money),
                "Currency code of other money has to be equal to currency code.")
            : new Money(Amount - otherMoney.Amount, Currency);
    }

    /// <summary>
    ///     Method to multiply the amount of money by a specified multiplier.
    /// </summary>
    /// <param name="multiplier">
    ///     The multiplier to apply to the amount of money.
    /// </param>
    /// <returns>
    ///     The result of the multiplication as a new Money object.
    /// </returns>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown if the multiplier is less than or equal to zero.
    /// </exception>
    public Money Multiply(decimal multiplier)
    {
        return !(multiplier > 0)
            ? throw new ValueObjectValidationException(
                nameof(Money),
                "Multiplier must be greater than zero.")
            : new Money(Amount * multiplier, Currency);
    }

    /// <summary>
    ///     Method to check if the currency codes of two Money objects are equal.
    /// </summary>
    /// <param name="otherMoney">
    ///     The other Money object to compare.
    /// </param>
    /// <returns>
    ///     True if the currency codes are equal, false otherwise.
    /// </returns>
    public bool AreEqualCurrency(Money otherMoney) => Currency.AreCodesEqual(otherMoney.GetCurrencyCode());
}
