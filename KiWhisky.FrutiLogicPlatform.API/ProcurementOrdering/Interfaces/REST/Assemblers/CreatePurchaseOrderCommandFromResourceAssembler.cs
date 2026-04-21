using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Assemblers;

/// <summary>
/// Assembler to convert CreatePurchaseOrderResource to CreatePurchaseOrderCommand.
/// </summary>
public static class CreatePurchaseOrderCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a CreatePurchaseOrderResource to a CreatePurchaseOrderCommand.
    /// </summary>
    /// <param name="resource">The creation purchase order resource.</param>
    /// <returns>The create purchase order command.</returns>
    public static CreatePurchaseOrderCommand ToCommandFromResource(CreatePurchaseOrderResource resource)
    {
        return new CreatePurchaseOrderCommand(
            resource.OrderCode,
            resource.CatalogIdBuyFrom,
            resource.Buyer
        );
    }
}
