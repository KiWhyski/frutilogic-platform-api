using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Exceptions;

/// <summary>
///     Exception class for handling Warehouse update failures.
/// </summary>
public class WarehouseFailedUpdateException : Exception
{
    /// <summary>
    ///     The name of the Entity that caused the exception.
    /// </summary>
    private const string Value = nameof(Warehouse);

    /// <summary>
    ///     Default constructor that creates a new instance of the WarehouseFailedUpdateException class.
    /// </summary>
    public WarehouseFailedUpdateException()
        : base($"Error trying to update the '{Value}'...")
    { }
    
    /// <summary>
    ///     Constructor that creates a new instance of the WarehouseFailedUpdateException class with additional details.
    /// </summary>
    /// <param name="details">
    ///     The details about why the Entity is considered invalid.
    /// </param>
    public WarehouseFailedUpdateException(string details)
        : base($"Error trying to update the '{Value}': {details}")
    { }
}
