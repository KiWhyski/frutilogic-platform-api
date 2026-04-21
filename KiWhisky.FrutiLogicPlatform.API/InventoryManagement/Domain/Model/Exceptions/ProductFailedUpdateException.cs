using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Exceptions;

/// <summary>
///     Exception class for handling Product update failures.
/// </summary>
public class ProductFailedUpdateException : Exception
{
    /// <summary>
    ///     The name of the Entity that caused the exception.
    /// </summary>
    private const string Value = nameof(Product);

    /// <summary>
    ///     Default constructor that creates a new instance of the ProductFailedUpdateException class.
    /// </summary>
    public ProductFailedUpdateException()
        : base($"Error trying to update the '{Value}'...")
    { }
    
    /// <summary>
    ///     Constructor that creates a new instance of the ProductFailedUpdateException class with additional details.
    /// </summary>
    /// <param name="details">
    ///     The details about why the Entity is considered invalid.
    /// </param>
    public ProductFailedUpdateException(string details)
        : base($"Error trying to update the '{Value}': {details}")
    { }
}
