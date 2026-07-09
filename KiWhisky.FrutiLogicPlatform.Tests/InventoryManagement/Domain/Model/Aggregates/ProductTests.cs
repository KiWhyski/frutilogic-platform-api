using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Extensions;

namespace KiWhisky.FrutiLogicPlatform.Tests.InventoryManagement.Domain.Model.Aggregates;

/// <summary>
///     Class for testing the Product Aggregate Root entity.
/// </summary>
public class ProductTests
{
    private Product CreateValidProduct()
    {
        const string name = "Manzana Fuji";
        const decimal price = 10.99m;
        var brand = EBrandNames.FruticultoresDelSur.GetDisplayName();
        const int minimumStock = 5;
        const decimal content = 1.00m;
        const string imageUrl = "https://www.example.com/image.jpg";
        const string ownerId = "1234567890";
        const string supplierId = "1234555";

        var unitPrice = new Money(price, new Currency(EValidCurrencyCodes.PEN.GetDisplayName()));
        var productMinimumStock = new ProductMinimumStock(minimumStock);
        var productContent = new ProductContent(content);
        var imageUrlObject = new ImageUrl(imageUrl);
        var accountId = new AccountId(ownerId);
        var supplierIdObject = new AccountId(supplierId);
        
        return new Product(name, EProductTypes.Apples, brand, unitPrice, productMinimumStock, productContent, imageUrlObject, accountId, supplierIdObject);
    }

    [Fact]
    public void Constructor_ValidInputs_ShouldCreateProduct()
    {
        // Arrange
        const string name = "Manzana Fuji";
        const decimal price = 10.99m;
        var brand = EBrandNames.FruticultoresDelSur.GetDisplayName();
        const int minimumStock = 5;
        const decimal content = 1.00m;
        const string imageUrl = "https://www.example.com/image.jpg";
        const string ownerId = "1234567890";
        const string supplierId = "1234555";

        // Act
        var unitPrice = new Money(price, new Currency(EValidCurrencyCodes.PEN.GetDisplayName()));
        var productMinimumStock = new ProductMinimumStock(minimumStock);
        var productContent = new ProductContent(content);
        var imageUrlObject = new ImageUrl(imageUrl);
        var accountId = new AccountId(ownerId);
        var supplierIdObject = new AccountId(supplierId);
        var product = new Product(name, EProductTypes.Apples, brand, unitPrice, productMinimumStock, productContent, imageUrlObject, accountId, supplierIdObject);

        // Assert
        Assert.Equal(name, product.Name);
        Assert.Equal(EProductTypes.Apples, product.Type);
        Assert.Equal(brand, product.Brand);
        Assert.Equal(unitPrice, product.UnitPrice);
        Assert.Equal(productMinimumStock, product.MinimumStock);
        Assert.Equal(productContent, product.Content);
        Assert.Equal(imageUrlObject, product.ImageUrl);
        Assert.Equal(accountId, product.AccountId);
        Assert.Equal(supplierIdObject, product.SupplierId);
        Assert.Equal(0, product.TotalStockInStore);
        Assert.False(product.IsInWarehouse);
    }

    [Theory]
    [InlineData(EProductTypes.Bananas)]
    [InlineData(EProductTypes.Oranges)]
    [InlineData(EProductTypes.Grapes)]
    [InlineData(EProductTypes.Mangos)]
    [InlineData(EProductTypes.Kiwis)]
    public void Constructor_ShouldAcceptAllValidProductTypesEnumValues(EProductTypes types)
    {
        // Arrange
        const string name = "Manzana Fuji";
        const decimal price = 10.99m;
        var brand = EBrandNames.FruticultoresDelSur.GetDisplayName();
        const int minimumStock = 5;
        const decimal content = 1.00m;
        const string imageUrl = "https://www.example.com/image.jpg";
        const string ownerId = "1234567890";
        const string supplierId = "1234555";
        
        // Act
        var unitPrice = new Money(price, new Currency(EValidCurrencyCodes.PEN.GetDisplayName()));
        var productMinimumStock = new ProductMinimumStock(minimumStock);
        var productContent = new ProductContent(content);
        var imageUrlObject = new ImageUrl(imageUrl);
        var accountId = new AccountId(ownerId);
        var supplierIdObject = new AccountId(supplierId);
        var product = new Product(name, types, brand, unitPrice, productMinimumStock, productContent, imageUrlObject, accountId, supplierIdObject);

        // Assert
        Assert.Equal(types, product.Type);
    }

    [Fact]
    public void UpdateTotalStockInStore_WithValidValue_ShouldUpdateStock()
    {
        // Arrange
        var product = CreateValidProduct();
        const int newStock = 50;

        // Act
        product.UpdateTotalStockInStore(newStock);

        // Assert
        Assert.Equal(newStock, product.TotalStockInStore);
        Assert.Equal(newStock, product.GetStockInStorage());
    }

    [Fact]
    public void UpdateTotalStockInStore_WithNegativeValue_ShouldThrowException()
    {
        // Arrange
        var product = CreateValidProduct();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => product.UpdateTotalStockInStore(-10));
    }

    [Fact]
    public void UpdateTotalStockInStore_WithZero_ShouldSetStockToZero()
    {
        // Arrange
        var product = CreateValidProduct();
        product.UpdateTotalStockInStore(50);

        // Act
        product.UpdateTotalStockInStore(0);

        // Assert
        Assert.Equal(0, product.TotalStockInStore);
    }

    [Fact]
    public void StoreProduct_WhenCalled_ShouldSetIsInWarehouseToTrue()
    {
        // Arrange
        var product = CreateValidProduct();
        Assert.False(product.IsInWarehouse);

        // Act
        product.StoreProduct();

        // Assert
        Assert.True(product.IsInWarehouse);
    }

    [Fact]
    public void UpdateInformation_WithValidCommand_ShouldUpdateProductInfo()
    {
        // Arrange
        var product = CreateValidProduct();
        const string newName = "Updated Product Name";
        const decimal newPrice = 25.50m;
        const int newMinimumStock = 10;
        const string newImageUrl = "https://www.example.com/new-image.jpg";

        var newUnitPrice = new Money(newPrice, new Currency(EValidCurrencyCodes.PEN.GetDisplayName()));
        var newMinimumStockObj = new ProductMinimumStock(newMinimumStock);
        
        var command = new UpdateProductInformationCommand(product.Id, newName, newUnitPrice, newMinimumStockObj, null);

        // Act
        product.UpdateInformation(command, newImageUrl);

        // Assert
        Assert.Equal(newName, product.Name);
        Assert.Equal(newUnitPrice, product.UnitPrice);
        Assert.Equal(newMinimumStock, product.MinimumStock.GetValue());
        Assert.Equal(newImageUrl, product.ImageUrl.GetValue());
    }

    [Fact]
    public void GetStockInStorage_AfterUpdate_ShouldReturnCorrectValue()
    {
        // Arrange
        var product = CreateValidProduct();
        const int expectedStock = 75;
        product.UpdateTotalStockInStore(expectedStock);

        // Act
        var actualStock = product.GetStockInStorage();

        // Assert
        Assert.Equal(expectedStock, actualStock);
    }

    [Fact]
    public void Constructor_WithNullSupplierId_ShouldCreateProduct()
    {
        // Arrange
        const string name = "Product Without Supplier";
        const decimal price = 15.00m;
        var brand = "Generic Brand";
        const int minimumStock = 3;
        const decimal content = 1.00m;
        const string imageUrl = "https://www.example.com/image.jpg";
        const string ownerId = "1234567890";

        var unitPrice = new Money(price, new Currency(EValidCurrencyCodes.USD.GetDisplayName()));
        var productMinimumStock = new ProductMinimumStock(minimumStock);
        var productContent = new ProductContent(content);
        var imageUrlObject = new ImageUrl(imageUrl);
        var accountId = new AccountId(ownerId);

        // Act
        var product = new Product(name, EProductTypes.Others, brand, unitPrice, productMinimumStock, productContent, imageUrlObject, accountId, null);

        // Assert
        Assert.Null(product.SupplierId);
        Assert.Equal(name, product.Name);
    }

    [Fact]
    public void UpdateMinimumStock_ShouldAssignUpdatedValue()
    {
        var product = CreateValidProduct();
        var command = new UpdateProductMinimumStockCommand(product.Id, 12);

        product.UpdateMinimumStock(command);

        Assert.Equal(12, product.MinimumStock.GetValue());
    }
}
