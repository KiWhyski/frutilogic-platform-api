using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Commands;

public record ReduceCatalogItemStockCommand(
    string CatalogId,
    ProductId ProductId,
    int Quantity
);
