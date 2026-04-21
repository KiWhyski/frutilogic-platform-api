using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using ESalesOrderStatuses = KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.ValueObjects.ESalesOrderStatuses;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Commands;

public record GenerateSalesOrderCommand(
    string orderCode, 
    PurchaseOrderId purchaseOrderId, 
    ICollection<SalesOrderItem> items, 
    ESalesOrderStatuses status, 
    CatalogId catalogToBuyFrom, 
    DateTime receiptDate, 
    DateTime completitionDate, 
    AccountId accountId
);

