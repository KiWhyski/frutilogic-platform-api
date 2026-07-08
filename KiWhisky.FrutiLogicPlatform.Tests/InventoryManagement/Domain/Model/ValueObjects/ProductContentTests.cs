using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;

namespace KiWhisky.FrutiLogicPlatform.Tests.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     Class for testing the ProductContent Value Object.
/// </summary>
public class ProductContentTests
{
    [Fact]
    public void Constructor_WithValidValue_ShouldCreateProductContent()
    {
        var content = new ProductContent(10.5m);
        Assert.Equal(10.5m, content.GetValue());
    }

    [Fact]
    public void Constructor_WithZeroValue_ShouldThrowException()
    {
        Assert.Throws<ValueObjectValidationException>(() => new ProductContent(0m));
    }

    [Fact]
    public void Constructor_WithNegativeValue_ShouldThrowException()
    {
        Assert.Throws<ValueObjectValidationException>(() => new ProductContent(-5.0m));
    }

    [Fact]
    public void Constructor_WithDefaultConstructor_ShouldCreateWithZeroValue()
    {
        var content = new ProductContent();
        Assert.Equal(0m, content.GetValue());
    }

    [Fact]
    public void GetValue_AfterCreation_ShouldReturnCorrectValue()
    {
        const decimal expectedValue = 750.5m;
        var content = new ProductContent(expectedValue);
        Assert.Equal(expectedValue, content.GetValue());
    }

    [Theory]
    [InlineData(0.1)]
    [InlineData(100)]
    [InlineData(999.99)]
    [InlineData(1500.75)]
    public void Constructor_WithVariousValidValues_ShouldCreateProductContent(decimal value)
    {
        var content = new ProductContent(value);
        Assert.Equal(value, content.GetValue());
    }

    [Fact]
    public void Equality_SameValue_ShouldBeEqual()
    {
        var content1 = new ProductContent(500m);
        var content2 = new ProductContent(500m);
        Assert.Equal(content1, content2);
    }

    [Fact]
    public void Equality_DifferentValue_ShouldNotBeEqual()
    {
        var content1 = new ProductContent(500m);
        var content2 = new ProductContent(600m);
        Assert.NotEqual(content1, content2);
    }
}
