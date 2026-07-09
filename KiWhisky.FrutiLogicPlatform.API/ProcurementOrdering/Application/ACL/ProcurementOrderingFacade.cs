using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.ACL;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Services;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Application.ACL;

public class ProcurementOrderingFacade : IProcurementOrderingFacade
{
    private readonly IPurchaseOrderRepository _orderRepository;
    private readonly IPurchaseOrderCommandService _commandService;

    public ProcurementOrderingFacade(
        IPurchaseOrderRepository orderRepository,
        IPurchaseOrderCommandService commandService)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
    }
    
    public async Task<PurchaseOrderResource> GetPurchaseOrderResourceAsync(string purchaseOrderId)
    {
        var order = await _orderRepository.GetByIdAsync(new PurchaseOrderId(purchaseOrderId))
                    ?? throw new InvalidOperationException($"Purchase order {purchaseOrderId} not found.");

        return new PurchaseOrderResource(
            id: order.PurchaseOrderId.GetId,
            orderCode: order.OrderCode,
            items: order.Items.Select(i => new PurchaseOrderItemResource(
                productId: i.ProductId.GetId,
                productName: i.ProductName,
                quantity: i.Quantity,
                unitPrice: i.UnitPrice,
                subTotal: i.CalculateSubTotal()
            )).ToList(),
            status: order.Status.ToString(),
            catalogIdBuyFrom: order.CatalogIdBuyFrom.GetId(),
            generationDate: order.GenerationDate,
            confirmationDate: order.ConfirmationDate,
            buyer: order.Buyer.GetId,
            isOrderSent: order.IsOrderSent,
            total: order.Items.Sum(i => i.CalculateSubTotal())
        );
    }

    public Task ConfirmPurchaseOrderAsync(string purchaseOrderId) =>
        _commandService.Handle(new ConfirmOrderCommand(purchaseOrderId));

    public Task ShipPurchaseOrderAsync(string purchaseOrderId) =>
        _commandService.Handle(new ShipOrderCommand(purchaseOrderId));

    public Task ReceivePurchaseOrderAsync(string purchaseOrderId) =>
        _commandService.Handle(new ReceiveOrderCommand(purchaseOrderId));

    public Task CancelPurchaseOrderAsync(string purchaseOrderId) =>
        _commandService.Handle(new CancelOrderCommand(purchaseOrderId));
}

