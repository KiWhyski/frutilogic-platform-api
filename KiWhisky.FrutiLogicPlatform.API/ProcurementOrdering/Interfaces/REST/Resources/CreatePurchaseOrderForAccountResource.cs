using System.Text.Json.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a purchase order for a specific account.
/// </summary>
public record CreatePurchaseOrderForAccountResource
{
    /// <summary>
    /// The order code.
    /// </summary>
    public string OrderCode { get; init; }
    
    /// <summary>
    /// The catalog ID to buy from.
    /// </summary>
    public string CatalogIdBuyFrom { get; init; }
    
    /// <summary>
    /// The optional index of the delivery address from the account's addresses.
    /// </summary>
    public int? AddressIndex { get; init; }

    public IReadOnlyCollection<CreatePurchaseOrderItemResource> Items { get; init; }

    [JsonConstructor]
    public CreatePurchaseOrderForAccountResource(
        string orderCode, 
        string catalogIdBuyFrom, 
        int? addressIndex = null,
        IReadOnlyCollection<CreatePurchaseOrderItemResource>? items = null)
    {
        OrderCode = orderCode;
        CatalogIdBuyFrom = catalogIdBuyFrom;
        AddressIndex = addressIndex;
        Items = items ?? Array.Empty<CreatePurchaseOrderItemResource>();
    }
}

public record CreatePurchaseOrderItemResource(string ProductId, int Quantity);
