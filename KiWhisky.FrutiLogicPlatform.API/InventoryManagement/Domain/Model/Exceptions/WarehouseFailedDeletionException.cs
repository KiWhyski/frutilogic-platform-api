using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Exceptions;

/// <summary>
///     Exception class for handling Warehouse deletion failures.
/// </summary>
public class WarehouseFailedDeletionException : Exception
{
    /// <summary>
    ///     The name of the Entity that caused the exception.
    /// </summary>
    private const string Value = nameof(Warehouse);

    /// <summary>
    ///     Default constructor that creates a new instance of the WarehouseFailedDeletionException class.
    /// </summary>
    public WarehouseFailedDeletionException()
        : base($"Error trying to delete the '{Value}'...")
    { }
    
    /// <summary>
    ///     Constructor that creates a new instance of the WarehouseFailedDeletionException class with additional details.
    /// </summary>
    /// <param name="details">
    ///     The details about why the Entity is considered invalid.
    /// </param>
    public WarehouseFailedDeletionException(string details)
        : base($"Error trying to delete the '{Value}': {details}")
    { }
}
