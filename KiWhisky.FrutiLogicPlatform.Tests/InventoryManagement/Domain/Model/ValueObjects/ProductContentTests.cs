using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;

namespace KiWhisky.FrutiLogicPlatform.Tests.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     Class for testing the ProductContent Value Object.
/// </summary>
public class ProductContentTests
{
    /// <summary>
    ///     Method to test the constructor of the ProductContent Value Object.
    /// </summary>
    [Fact]
    public void Constructor_ValidName_ShouldCreateInstance()
    {
        var content = new ProductContent(10.00m);
        Assert.Equal(10, content.GetValue());
    }

    /// <summary>
    ///     Method to test the constructor of the ProductContent Value Object.
    /// </summary>
    [Fact]
    public void Constructor_EmptyName_ShouldThrowException()
    {
        Assert.Throws<ValueObjectValidationException>(() => new ProductContent());
    }
}
