using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.ACL;

public interface IProcurementOrderingFacade
{
    Task<PurchaseOrderResource> GetPurchaseOrderResourceAsync(string purchaseOrderId);
}
