using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.Tests.InventoryManagement.Domain.Model.ValueObjects;

public class ProductExpirationDateTests
{
    [Fact]
    public void Constructor_WithValidDate_ShouldCreateProductExpirationDate()
    {
        var futureDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30));
        var expirationDate = new ProductExpirationDate(futureDate);
        Assert.Equal(futureDate, expirationDate.GetValue());
    }

    [Fact]
    public void Constructor_WithNull_ShouldCreateWithDefaultValue()
    {
        var expirationDate = new ProductExpirationDate(null);
        Assert.Equal(new DateOnly(), expirationDate.GetValue());
    }

    [Fact]
    public void Constructor_WithToday_ShouldCreateProductExpirationDate()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var expirationDate = new ProductExpirationDate(today);
        Assert.Equal(today, expirationDate.GetValue());
    }

    [Fact]
    public void Constructor_WithPastDate_ShouldCreateProductExpirationDate()
    {
        var pastDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-30));
        var expirationDate = new ProductExpirationDate(pastDate);
        Assert.Equal(pastDate, expirationDate.GetValue());
    }

    [Fact]
    public void GetValue_AfterCreation_ShouldReturnCorrectValue()
    {
        var expectedDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6));
        var expirationDate = new ProductExpirationDate(expectedDate);
        Assert.Equal(expectedDate, expirationDate.GetValue());
    }

    [Fact]
    public void ToString_WithValidDate_ShouldReturnFormattedString()
    {
        var date = new DateOnly(2025, 12, 31);
        var expirationDate = new ProductExpirationDate(date);
        Assert.Equal("2025-12-31", expirationDate.ToString());
    }

    [Fact]
    public void ToString_WithAnotherDate_ShouldReturnFormattedString()
    {
        var date = new DateOnly(2024, 6, 15);
        var expirationDate = new ProductExpirationDate(date);
        Assert.Equal("2024-06-15", expirationDate.ToString());
    }

    [Fact]
    public void Equality_SameValue_ShouldBeEqual()
    {
        var date = DateOnly.FromDateTime(DateTime.Today.AddDays(60));
        var expirationDate1 = new ProductExpirationDate(date);
        var expirationDate2 = new ProductExpirationDate(date);
        Assert.Equal(expirationDate1, expirationDate2);
    }

    [Fact]
    public void Equality_DifferentValue_ShouldNotBeEqual()
    {
        var date1 = DateOnly.FromDateTime(DateTime.Today.AddDays(30));
        var date2 = DateOnly.FromDateTime(DateTime.Today.AddDays(60));
        var expirationDate1 = new ProductExpirationDate(date1);
        var expirationDate2 = new ProductExpirationDate(date2);
        Assert.NotEqual(expirationDate1, expirationDate2);
    }

    [Fact]
    public void Constructor_WithDefaultConstructor_ShouldCreateWithDefaultValue()
    {
        var expirationDate = new ProductExpirationDate();
        Assert.Equal(new DateOnly(), expirationDate.GetValue());
    }
}
