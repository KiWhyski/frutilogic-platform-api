using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Events;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.Tests.InventoryManagement.Domain.Model.Aggregates;

public class InventoryAlertTests
{
    private static Inventory CreateInventory(int quantity) =>
        new(ObjectId.GenerateNewId(), ObjectId.GenerateNewId(), new ProductStock(quantity));

    [Fact]
    public void DecreaseToZero_EmitsOnlyOutOfStockEvent()
    {
        var inventory = CreateInventory(5);

        inventory.DecreaseStockFromProduct(5, 5, new AccountId("account-1"));

        Assert.Equal(EProductStates.OutOfStock, inventory.CurrentState);
        Assert.Single(inventory.DomainEvents);
        Assert.IsType<ProductWithoutStockDetectedEvent>(inventory.DomainEvents.Single());
    }

    [Fact]
    public void DecreaseWhileAlreadyLow_DoesNotEmitDuplicateEvent()
    {
        var inventory = CreateInventory(10);
        inventory.DecreaseStockFromProduct(5, 5, new AccountId("account-1"));
        inventory.ClearDomainEvents();

        inventory.DecreaseStockFromProduct(1, 5, new AccountId("account-1"));

        Assert.Equal(EProductStates.LowStock, inventory.CurrentState);
        Assert.Empty(inventory.DomainEvents);
    }

    [Fact]
    public void AddStock_RecalculatesStateFromResultingQuantity()
    {
        var inventory = CreateInventory(5);
        inventory.DecreaseStockFromProduct(5, 5, new AccountId("account-1"));
        inventory.ClearDomainEvents();

        inventory.AddStockToProduct(3, 5);

        Assert.Equal(EProductStates.LowStock, inventory.CurrentState);
        Assert.Equal(3, inventory.GetStock());
    }

    [Fact]
    public void Constructor_PreservesExpirationDate()
    {
        var expiration = new ProductExpirationDate(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)));

        var inventory = new Inventory(
            ObjectId.GenerateNewId(), ObjectId.GenerateNewId(), new ProductStock(2), expiration);

        Assert.Equal(expiration, inventory.ExpirationDate);
    }
}
