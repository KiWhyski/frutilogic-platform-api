using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Queries;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Services
{
    public interface ISalesOrderQueryService
    {
        Task<IEnumerable<SalesOrder>> Handle(GetAllSalesOrdersByBuyerIdQuery query);
        Task<IEnumerable<SalesOrder>> Handle(GetAllSalesOrdersQuery query);
        Task<IEnumerable<SalesOrder>> Handle(GetAllSalesOrdersBySupplierIdQuery query);
    }
}

