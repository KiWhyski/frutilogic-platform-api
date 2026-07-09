using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.ACL;

public interface IProcurementOrderingFacade
{
    Task<PurchaseOrderResource> GetPurchaseOrderResourceAsync(string purchaseOrderId);
    Task ConfirmPurchaseOrderAsync(string purchaseOrderId);
    Task ShipPurchaseOrderAsync(string purchaseOrderId);
    Task ReceivePurchaseOrderAsync(string purchaseOrderId);
    Task CancelPurchaseOrderAsync(string purchaseOrderId);
}
