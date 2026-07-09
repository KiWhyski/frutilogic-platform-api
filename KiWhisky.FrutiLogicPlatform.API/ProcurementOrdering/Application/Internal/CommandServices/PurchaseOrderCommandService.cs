using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.ACL.Services;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Application.Internal.CommandServices;

/// <summary>
/// Application service responsible for handling all commands related to <see cref="PurchaseOrder"/>.
/// Implements business logic at the application layer and delegates persistence to repositories.
/// </summary>
public class PurchaseOrderCommandService : IPurchaseOrderCommandService
{
    private readonly IPurchaseOrderRepository _orderRepository;
    private readonly ICatalogRepository _catalogRepository;
    private readonly ICatalogCommandService _catalogCommandService;
    private readonly IPaymentAndSubscriptionsFacade _paymentFacade;

    public PurchaseOrderCommandService(
        IPurchaseOrderRepository orderRepository,
        ICatalogRepository catalogRepository,
        ICatalogCommandService catalogCommandService,
        IPaymentAndSubscriptionsFacade paymentFacade)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        _catalogCommandService = catalogCommandService ?? throw new ArgumentNullException(nameof(catalogCommandService));
        _paymentFacade = paymentFacade ?? throw new ArgumentNullException(nameof(paymentFacade));
    }

    /// <summary>
    /// Handles the CreatePurchaseOrderCommand.
    /// Creates a new purchase order and optionally assigns a delivery address if addressIndex is provided.
    /// </summary>
    public async Task<PurchaseOrderId> Handle(CreatePurchaseOrderCommand command)
    {
        var catalog = await _catalogRepository.GetByIdAsync(new CatalogId(command.catalogIdBuyFrom))
                      ?? throw new InvalidOperationException("The selected catalog does not exist.");
        if (!catalog.IsPublished)
            throw new InvalidOperationException("The selected catalog is not published.");
        if (catalog.OwnerAccount.GetId.Equals(command.buyer, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("The catalog owner cannot buy from their own catalog.");
        if (command.items == null || command.items.Count == 0)
            throw new InvalidOperationException("A purchase order must contain at least one item.");
        if (command.items.GroupBy(i => i.ProductId, StringComparer.OrdinalIgnoreCase).Any(g => g.Count() > 1))
            throw new InvalidOperationException("A product cannot be repeated in the purchase order.");

        var order = new PurchaseOrder(command);
        foreach (var requestedItem in command.items)
        {
            if (requestedItem.Quantity <= 0)
                throw new InvalidOperationException("Item quantities must be greater than zero.");

            var catalogItem = catalog.CatalogItems.FirstOrDefault(i =>
                i.ProductId.GetId.Equals(requestedItem.ProductId, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException(
                    $"Product '{requestedItem.ProductId}' does not belong to the selected catalog.");
            if (!catalogItem.HasSufficientStock(requestedItem.Quantity))
                throw new InvalidOperationException($"Insufficient stock for product '{catalogItem.ProductName}'.");

            order.AddItem(catalogItem, requestedItem.Quantity);
        }

        if (command.addressIndex.HasValue)
        {
            var address = await _paymentFacade.GetAccountAddressAsync(
                command.buyer,
                command.addressIndex.Value
            );

            if (address == null)
                throw new InvalidOperationException(
                    $"Address at index {command.addressIndex.Value} not found for account {command.buyer}.");

            var deliveryAddress = DeliveryAddress.FromAddress(address);
            order.SetDeliveryAddress(deliveryAddress);
        }

        foreach (var item in order.Items)
            catalog.ReduceItemStock(new ReduceCatalogItemStockCommand(
                catalog.CatalogId.GetId(), item.ProductId, item.Quantity));

        await _catalogRepository.UpdateAsync(catalog);
        try
        {
            await _orderRepository.AddAsync(order);
        }
        catch
        {
            foreach (var item in order.Items)
                catalog.RestoreItemStock(item.ProductId.GetId, item.Quantity);
            await _catalogRepository.UpdateAsync(catalog);
            await _orderRepository.DeleteAsync(order.PurchaseOrderId.GetId);
            throw;
        }

        return order.PurchaseOrderId;
    }

    /// <summary>
    /// Handles the AddItemToOrderCommand.
    /// Adds a catalog item to an existing purchase order with the specified quantity.
    /// Also reduces the available stock of that item in the catalog.
    /// </summary>
    public async Task Handle(AddItemToOrderCommand command)
    {
        var order = await GetOrderByIdAsync(command.OrderId);

        var catalog = await _catalogRepository.GetByIdAsync(order.CatalogIdBuyFrom)
                      ?? throw new InvalidOperationException("Associated catalog not found.");

        var catalogItem = catalog.CatalogItems
            .FirstOrDefault(i => i.ProductId.GetId.Trim().ToLower() == command.ProductId.Trim().ToLower())
            ?? throw new InvalidOperationException($"Product with ID '{command.ProductId}' not found in catalog.");
        
        if (!catalogItem.HasSufficientStock(command.Quantity))
            throw new InvalidOperationException($"Insufficient stock for product '{catalogItem.ProductName}'.");
        
        order.AddItem(catalogItem, command.Quantity);
        await _orderRepository.UpdateAsync(order);
        
        var reduceStockCommand = new ReduceCatalogItemStockCommand(
            catalog.CatalogId.GetId(),
            catalogItem.ProductId,
            command.Quantity
        );

        await _catalogCommandService.Handle(reduceStockCommand);
    }

    /// <summary>
    /// Handles the RemoveItemFromOrderCommand.
    /// </summary>
    public async Task Handle(RemoveItemFromOrderCommand command)
    {
        var order = await GetOrderByIdAsync(command.orderId);
        var removedItem = order.RemoveItem(command);
        var catalog = await _catalogRepository.GetByIdAsync(order.CatalogIdBuyFrom)
                      ?? throw new InvalidOperationException("Associated catalog not found.");
        catalog.RestoreItemStock(removedItem.ProductId.GetId, removedItem.Quantity);
        await _catalogRepository.UpdateAsync(catalog);
        await _orderRepository.UpdateAsync(order);
    }

    /// <summary>
    /// Handles the ConfirmOrderCommand.
    /// </summary>
    public async Task Handle(ConfirmOrderCommand command)
    {
        var order = await GetOrderByIdAsync(command.orderId);
        order.ConfirmOrder();
        await _orderRepository.UpdateAsync(order);
    }

    /// <summary>
    /// Handles the ShipOrderCommand.
    /// </summary>
    public async Task Handle(ShipOrderCommand command)
    {
        var order = await GetOrderByIdAsync(command.orderId);
        order.ShipOrder();
        await _orderRepository.UpdateAsync(order);
    }

    /// <summary>
    /// Handles the ReceiveOrderCommand.
    /// </summary>
    public async Task Handle(ReceiveOrderCommand command)
    {
        var order = await GetOrderByIdAsync(command.orderId);
        order.ReceiveOrder();
        await _orderRepository.UpdateAsync(order);
    }

    /// <summary>
    /// Handles the CancelOrderCommand.
    /// </summary>
    public async Task Handle(CancelOrderCommand command)
    {
        var order = await GetOrderByIdAsync(command.orderId);
        order.CancelOrder();
        var catalog = await _catalogRepository.GetByIdAsync(order.CatalogIdBuyFrom)
                      ?? throw new InvalidOperationException("Associated catalog not found.");
        foreach (var item in order.Items)
            catalog.RestoreItemStock(item.ProductId.GetId, item.Quantity);
        await _catalogRepository.UpdateAsync(catalog);
        await _orderRepository.UpdateAsync(order);
    }

    private async Task<PurchaseOrder> GetOrderByIdAsync(string orderId)
    {
        var id = new PurchaseOrderId(orderId);
        var order = await _orderRepository.GetByIdAsync(id);
        return order ?? throw new InvalidOperationException($"Order with ID '{orderId}' not found.");
    }
}
