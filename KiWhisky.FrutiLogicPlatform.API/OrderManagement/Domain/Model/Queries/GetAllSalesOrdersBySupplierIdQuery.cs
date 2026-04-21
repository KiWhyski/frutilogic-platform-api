using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Queries;


public record GetAllSalesOrdersBySupplierIdQuery(AccountId supplierId);

