using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Command to update product information.
/// </summary>
public record UpdateProductInformationCommand(
        ObjectId ProductId,
        string Name,
        Money UnitPrice,
        ProductMinimumStock MinimumStock,
        IFormFile? Image
    );
