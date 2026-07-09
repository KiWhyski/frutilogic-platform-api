using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.Tests.ProcurementOrdering.Domain.Model.Aggregates;

public class PurchaseOrderStockTests
{
    [Fact]
    public void RemoveItem_ReturnsReservedItem_ForStockRestoration()
    {
        var catalog = CreateCatalogWithItem(stock: 8);
        var order = new PurchaseOrder("PO-1", catalog.CatalogId, new AccountId("buyer"));
        order.AddItem(catalog.CatalogItems.Single(), 3);

        var removed = order.RemoveItem(new RemoveItemFromOrderCommand(
            order.PurchaseOrderId.GetId,
            catalog.CatalogItems.Single().ProductId.GetId));

        Assert.Equal(3, removed.Quantity);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void RestoreItemStock_AddsCanceledOrRemovedQuantity()
    {
        var catalog = CreateCatalogWithItem(stock: 8);
        var productId = catalog.CatalogItems.Single().ProductId;
        catalog.ReduceItemStock(new ReduceCatalogItemStockCommand(catalog.CatalogId.GetId(), productId, 3));

        catalog.RestoreItemStock(productId.GetId, 3);

        Assert.Equal(8, catalog.CatalogItems.Single().Stock);
    }

    [Fact]
    public void CancelOrder_CannotRestoreTwice()
    {
        var catalog = CreateCatalogWithItem(stock: 8);
        var order = new PurchaseOrder("PO-2", catalog.CatalogId, new AccountId("buyer"));
        order.AddItem(catalog.CatalogItems.Single(), 1);
        order.CancelOrder();

        var error = Assert.Throws<InvalidOperationException>(() => order.CancelOrder());

        Assert.Contains("already canceled", error.Message);
    }

    private static Catalog CreateCatalogWithItem(int stock)
    {
        var catalog = new Catalog(
            "Catalog",
            "Description",
            new AccountId("supplier"),
            new Email("supplier@example.com"),
            "507f1f77bcf86cd799439011");
        catalog.AddItem(
            "507f191e810c19729de860ea",
            "Apples",
            10m,
            "USD",
            string.Empty,
            stock);
        return catalog;
    }
}
