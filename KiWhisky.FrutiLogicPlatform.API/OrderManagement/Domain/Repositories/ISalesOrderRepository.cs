using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Repositories;

/// <summary>
///     Repository interface for managing SalesOrder entities.
/// </summary>
public interface ISalesOrderRepository : IBaseRepository<SalesOrder>
{
    Task<SalesOrder> GenerateSalesOrder(GenerateSalesOrderCommand command);
    Task<IEnumerable<SalesOrder>> GetAllSalesOrdersByBuyerId(AccountId buyerId);
    Task<IEnumerable<SalesOrder>> GetAllSalesOrdersBySupplierId(AccountId supplierId);
    Task<IEnumerable<SalesOrder>> GetAllSalesOrders();
    Task<SalesOrder> GetByIdAsync(string id);
    Task<SalesOrder?> GetByPurchaseOrderIdAsync(PurchaseOrderId purchaseOrderId);
}
