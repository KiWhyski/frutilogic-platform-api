using System.ComponentModel.DataAnnotations;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Interfaces.REST.Resources
{
    public record GeneratePurchaseOrderRequest(
        [Required] string OrderCode,
        [Required] string CatalogToBuyFrom,
        DateTime? ReceiptDate,
        DateTime? CompletitionDate,
        [Required] ICollection<SalesOrderItemResource> Items,
        bool IsAutomatic = false
    );
}

