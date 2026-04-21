using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;

/// <summary>
///     Command service interface for handling product-related commands.
/// </summary>
public interface IProductCommandService
{
    /// <summary>
    ///     Method to handle the registration of a new product.
    /// </summary>
    /// <param name="command">
    ///     The command containing the details for registering a new product.
    /// </param>
    /// <returns>
    ///     The newly registered product.
    ///     Or null if the product could not be registered.
    /// </returns>
    Task<Product?> Handle(RegisterProductCommand command);
    
    /// <summary>
    ///     Method to handle the update of product information.
    /// </summary>
    /// <param name="command">
    ///     The command containing the details for updating the product information.
    /// </param>
    /// <returns>
    ///     The updated product.
    ///     Or null if the product could not be updated.
    /// </returns>
    Task<Product?> Handle(UpdateProductInformationCommand command);

    /// <summary>
    ///     Method to handle the update of the minimum stock related to a product.
    /// </summary>
    /// <param name="command">
    ///     The command containing the details for updating the minimum stock.
    /// </param>
    /// <returns>
    ///     The updated product.
    ///     Or null if the product could not be updated.   
    /// </returns>
    Task<Product?> Handle(UpdateProductMinimumStockCommand command);
    
    /// <summary>
    ///     Method to delete a product.
    /// </summary>
    /// <param name="command">
    ///     The command containing the details for deleting a product.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     No return value.
    /// </returns>
    Task Handle(DeleteProductCommand command);
}
