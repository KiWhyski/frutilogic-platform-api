using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Exceptions;

/// <summary>
///     Exception class for handling Warehouse creation failures.
/// </summary>
public class WarehouseFailedCreationException : Exception
{
    /// <summary>
    ///     The name of the Entity that caused the exception.
    /// </summary>
    private const string Value = nameof(Warehouse);

    /// <summary>
    ///     Default constructor that creates a new instance of the WarehouseFailedCreationException class.
    /// </summary>
    public WarehouseFailedCreationException()
        : base($"Error trying to create the '{Value}'...")
    { }
    
    /// <summary>
    ///     Constructor that creates a new instance of the WarehouseFailedCreationException class with additional details.
    /// </summary>
    /// <param name="details">
    ///     The details about why the Entity is considered invalid.
    /// </param>
    public WarehouseFailedCreationException(string details)
        : base($"Error trying to create the '{Value}': {details}")
    { }
}
