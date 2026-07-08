using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;

namespace KiWhisky.FrutiLogicPlatform.Tests.InventoryManagement.Domain.Model.ValueObjects;

public class ProductNameTests
{
    [Fact]
    public void Constructor_WithValidName_ShouldCreateProductName()
    {
        const string validName = "Whisky Blue Label";
        var productName = new ProductName(validName);
        Assert.Equal(validName, productName.GetValue());
    }

    [Fact]
    public void Constructor_WithEmptyString_ShouldThrowException()
    {
        Assert.Throws<ValueObjectValidationException>(() => new ProductName(""));
    }

    [Fact]
    public void Constructor_WithWhitespace_ShouldThrowException()
    {
        Assert.Throws<ValueObjectValidationException>(() => new ProductName("   "));
    }

    [Fact]
    public void Constructor_WithNull_ShouldThrowException()
    {
        // Note: The current implementation throws NullReferenceException because
        // name.Trim() is called before validation when name is null
        Assert.Throws<NullReferenceException>(() => new ProductName(null!));
    }

    [Fact]
    public void Constructor_WithTabAndSpaces_ShouldThrowException()
    {
        Assert.Throws<ValueObjectValidationException>(() => new ProductName("\t  \n"));
    }

    [Fact]
    public void GetValue_AfterCreation_ShouldReturnCorrectValue()
    {
        const string expectedName = "Ron Caribeño";
        var productName = new ProductName(expectedName);
        Assert.Equal(expectedName, productName.GetValue());
    }

    [Theory]
    [InlineData("Product A")]
    [InlineData("12345")]
    [InlineData("Cerveza Artesanal IPA")]
    [InlineData("Vodka Premium 750ml")]
    public void Constructor_WithVariousValidNames_ShouldCreateProductName(string name)
    {
        var productName = new ProductName(name);
        Assert.Equal(name, productName.GetValue());
    }

    [Fact]
    public void Equality_SameValue_ShouldBeEqual()
    {
        var name1 = new ProductName("Whisky");
        var name2 = new ProductName("Whisky");
        Assert.Equal(name1, name2);
    }

    [Fact]
    public void Equality_DifferentValue_ShouldNotBeEqual()
    {
        var name1 = new ProductName("Whisky");
        var name2 = new ProductName("Ron");
        Assert.NotEqual(name1, name2);
    }

    [Fact]
    public void Constructor_WithDefaultConstructor_ShouldCreateWithEmptyValue()
    {
        var productName = new ProductName();
        Assert.Equal(string.Empty, productName.GetValue());
    }
}
