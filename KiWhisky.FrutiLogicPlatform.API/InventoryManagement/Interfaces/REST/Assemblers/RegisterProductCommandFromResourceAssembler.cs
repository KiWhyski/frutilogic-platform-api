using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;

/// <summary>
///     Static assembler class to convert RegisterProductResource to RegisterProductCommand.
/// </summary>
public static class RegisterProductCommandFromResourceAssembler
{
    /// <summary>
    ///     Static method to convert RegisterProductResource to RegisterProductCommand.   
    /// </summary>
    public static RegisterProductCommand ToCommandFromResource(RegisterProductResource resource, string accountId)
    {
        if (resource is null)
            throw new ArgumentException("Product payload is required.");
        if (string.IsNullOrWhiteSpace(resource.Name))
            throw new ArgumentException("Name is required.");
        if (string.IsNullOrWhiteSpace(resource.Type))
            throw new ArgumentException("Type is required.");
        if (string.IsNullOrWhiteSpace(resource.Brand))
            throw new ArgumentException("Brand is required.");
        if (string.IsNullOrWhiteSpace(resource.Code))
            throw new ArgumentException("Currency code is required.");
        if (!Enum.TryParse<EProductTypes>(resource.Type, ignoreCase: true, out var productType))
            throw new ArgumentException($"Invalid product type: {resource.Type}");

        var content = resource.Content > 0 ? resource.Content : 1m;
        var minimumStock = resource.MinimumStock > 0 ? resource.MinimumStock : 1;

        return new RegisterProductCommand(
                resource.Name.Trim(),
                productType,
                resource.Brand.Trim(),
                new Money(resource.UnitPrice, new Currency(resource.Code)),
                new ProductMinimumStock(minimumStock),
                new ProductContent(content),
                resource.Image,
                new AccountId(accountId),
                new AccountId(resource.SupplierId ?? string.Empty)
            );
    }
}
