using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.ValueObjects;

public interface IOrderManagementContextFacade
{
    Task<SalesOrder?> GenerateSalesOrderAsync(GenerateSalesOrderCommand command);
    Task<IEnumerable<SalesOrder>> GetSalesOrdersByBuyerAsync(AccountId buyerId);
}

