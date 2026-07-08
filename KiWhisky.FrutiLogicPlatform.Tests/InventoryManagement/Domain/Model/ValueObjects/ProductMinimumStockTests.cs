using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;

namespace KiWhisky.FrutiLogicPlatform.Tests.InventoryManagement.Domain.Model.ValueObjects;

public class ProductMinimumStockTests
{
    [Fact]
    public void Constructor_WithValidValue_ShouldCreateProductMinimumStock()
    {
        const int validMinimumStock = 10;
        var minimumStock = new ProductMinimumStock(validMinimumStock);
        Assert.Equal(validMinimumStock, minimumStock.GetValue());
    }

    [Fact]
    public void Constructor_WithOne_ShouldCreateProductMinimumStock()
    {
        var minimumStock = new ProductMinimumStock(1);
        Assert.Equal(1, minimumStock.GetValue());
    }

    [Fact]
    public void Constructor_WithZero_ShouldThrowException()
    {
        Assert.Throws<ValueObjectValidationException>(() => new ProductMinimumStock(0));
    }

    [Fact]
    public void Constructor_WithNegativeValue_ShouldThrowException()
    {
        Assert.Throws<ValueObjectValidationException>(() => new ProductMinimumStock(-5));
    }

    [Fact]
    public void UpdateMinimumStock_WithValidValue_ShouldReturnNewMinimumStock()
    {
        var minimumStock = new ProductMinimumStock(5);
        var newMinimumStock = minimumStock.UpdateMinimumStock(15);
        Assert.Equal(15, newMinimumStock.GetValue());
    }

    [Fact]
    public void UpdateMinimumStock_WithZero_ShouldThrowException()
    {
        var minimumStock = new ProductMinimumStock(5);
        Assert.Throws<ValueObjectValidationException>(() => minimumStock.UpdateMinimumStock(0));
    }

    [Fact]
    public void UpdateMinimumStock_WithNegativeValue_ShouldThrowException()
    {
        var minimumStock = new ProductMinimumStock(5);
        Assert.Throws<ValueObjectValidationException>(() => minimumStock.UpdateMinimumStock(-10));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(50)]
    [InlineData(100)]
    public void Constructor_WithVariousValidValues_ShouldCreateProductMinimumStock(int value)
    {
        var minimumStock = new ProductMinimumStock(value);
        Assert.Equal(value, minimumStock.GetValue());
    }

    [Fact]
    public void Equality_SameValue_ShouldBeEqual()
    {
        var minimumStock1 = new ProductMinimumStock(10);
        var minimumStock2 = new ProductMinimumStock(10);
        Assert.Equal(minimumStock1, minimumStock2);
    }

    [Fact]
    public void Equality_DifferentValue_ShouldNotBeEqual()
    {
        var minimumStock1 = new ProductMinimumStock(10);
        var minimumStock2 = new ProductMinimumStock(20);
        Assert.NotEqual(minimumStock1, minimumStock2);
    }

    [Fact]
    public void Constructor_WithDefaultConstructor_ShouldCreateWithZeroValue()
    {
        var minimumStock = new ProductMinimumStock();
        Assert.Equal(0, minimumStock.GetValue());
    }
}
