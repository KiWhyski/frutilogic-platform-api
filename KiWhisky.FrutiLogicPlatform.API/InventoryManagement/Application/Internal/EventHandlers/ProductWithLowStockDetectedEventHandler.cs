using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Application.ACL;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Events;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.Shared.Application.Internal.EventHandlers;
using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Application.Internal.EventHandlers;

/// <summary>
///     Handler for the <see cref="ProductWithLowStockDetectedEvent"/> event.
/// </summary>
/// <param name="alertsAndNotificationsService">
///     The external service for creating alerts and notifications.
/// </param>
public class ProductWithLowStockDetectedEventHandler(
        ExternalAlertsAndNotificationsService alertsAndNotificationsService,
        IInventoryRepository inventoryRepository,
        IProductRepository productRepository,
        IWarehouseRepository warehouseRepository
    ) : IEventHandler<ProductWithLowStockDetectedEvent>
{
    /// <summary>
    ///     Method to handle the event.
    /// </summary>
    /// <param name="notification">
    ///     The event notification.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    public async Task Handle(ProductWithLowStockDetectedEvent notification, CancellationToken cancellationToken)
    {
        var inventoryId = await On(notification);
        Console.WriteLine($"[ProductLowStock] Inventory ID: {inventoryId}");
    }
    
    private async Task<string> On(ProductWithLowStockDetectedEvent domainEvent)
    {
        Inventory inventory;

        if (domainEvent.ExpirationDate != null)
        {
            inventory = await inventoryRepository
                .GetByProductIdWarehouseIdAndExpirationDateAsync(
                    new ObjectId(domainEvent.ProductId),
                    new ObjectId(domainEvent.WarehouseId),
                    domainEvent.ExpirationDate
                ) ?? throw new ArgumentException("The inventory does not exist.");
        }
        else
        {
            inventory = await inventoryRepository
                .GetByProductIdWarehouseIdAsync(
                    new ObjectId(domainEvent.ProductId),
                    new ObjectId(domainEvent.WarehouseId)
                ) ?? throw new ArgumentException("The inventory does not exist.");
        }

        var product = await productRepository.FindByIdAsync(domainEvent.ProductId.ToString()) 
                      ?? throw new ArgumentException("The product does not exist.");
        var warehouse = await warehouseRepository.FindByIdAsync(domainEvent.WarehouseId.ToString()) 
                        ?? throw new ArgumentException("The warehouse does not exist.");
        
        var title = "Low Stock Level Warning";
        var message = $"Product {product.Name} in warehouse {warehouse.Name} has reached the minimum stock level.";;
        var severity = "Warning";
        var type = "ProductLowStock";

        await alertsAndNotificationsService.CreateAlertAsync(
            title,
            message,
            severity,
            type,
            domainEvent.AccountId,
            inventory.Id.ToString(),
            new AlertDetails(
                product.Id.ToString(),
                product.Name,
                warehouse.Id.ToString(),
                warehouse.Name,
                inventory.GetStock(),
                product.MinimumStock.GetValue(),
                inventory.ExpirationDate?.ToString())
        );
        
        return inventory.Id.ToString();
    }
}
