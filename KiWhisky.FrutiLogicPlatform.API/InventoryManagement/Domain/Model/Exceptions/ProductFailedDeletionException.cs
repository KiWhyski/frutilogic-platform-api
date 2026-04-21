using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Exceptions;

/// <summary>
///     Exception class for handling Product deletion failures.
/// </summary>
public class ProductFailedDeletionException : Exception
{
    /// <summary>
    ///     The name of the Entity that caused the exception.
    /// </summary>
    private const string Value = nameof(Product);

    /// <summary>
    ///     Default constructor that creates a new instance of the ProductFailedCreationException class.
    /// </summary>
    public ProductFailedDeletionException()
        : base($"Error trying to delete the '{Value}'...")
    { }
    
    /// <summary>
    ///     Constructor that creates a new instance of the ProductFailedCreationException class with additional details.
    /// </summary>
    /// <param name="details">
    ///     The details about why the Entity is considered invalid.
    /// </param>
    public ProductFailedDeletionException(string details)
        : base($"Error trying to delete the '{Value}': {details}")
    { }
}
