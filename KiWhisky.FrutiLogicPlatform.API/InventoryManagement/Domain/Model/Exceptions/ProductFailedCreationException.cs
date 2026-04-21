using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Exceptions;

/// <summary>
///     Exception class for handling Product creation failures.
/// </summary>
public class ProductFailedCreationException : Exception
{
    /// <summary>
    ///     The name of the Entity that caused the exception.
    /// </summary>
    private const string Value = nameof(Product);

    /// <summary>
    ///     Default constructor that creates a new instance of the ProductFailedCreationException class.
    /// </summary>
    public ProductFailedCreationException()
        : base($"Error trying to create the '{Value}'...")
    { }
    
    /// <summary>
    ///     Constructor that creates a new instance of the ProductFailedCreationException class with additional details.
    /// </summary>
    /// <param name="details">
    ///     The details about why the Entity is considered invalid.
    /// </param>
    public ProductFailedCreationException(string details)
        : base($"Error trying to create the '{Value}': {details}")
    { }
}
