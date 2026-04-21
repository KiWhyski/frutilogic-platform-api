using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     The value object representing the unique identifier for a purchase order.
/// </summary>
[BsonSerializer(typeof(PurchaseOrderIdSerializer))]
public record PurchaseOrderId()
{
    /// <summary>
    ///     The unique identifier for a purchase order.
    /// </summary>
    private string Id { get; } = string.Empty;

    /// <summary>
    ///     The default constructor for the PurchaseOrderId value object.
    /// </summary>
    /// <param name="id">
    ///     The identifier for the purchase order.
    /// </param>
    /// <exception cref="ValueObjectValidationException">
    ///     Thrown when the provided id is null or an empty string.
    /// </exception>
    public PurchaseOrderId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ValueObjectValidationException(nameof(PurchaseOrderId), "Purchase Order ID must be a non-empty string.");
        }
        
        Id = id;
    }

    /// <summary>
    ///     The method to retrieve the string representation of the PurchaseOrderId value object.
    /// </summary>
    public string GetId => Id;
}
