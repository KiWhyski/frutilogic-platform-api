using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     The MongoDB serializer for the <see cref="PurchaseOrderId" /> value object.
/// </summary>
public class PurchaseOrderIdSerializer : IBsonSerializer<PurchaseOrderId>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PurchaseOrderId value)
    {
        context.Writer.WriteString(value.GetId);
    }

    public PurchaseOrderId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new PurchaseOrderId(value ?? throw new InvalidOperationException());
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (PurchaseOrderId)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(PurchaseOrderId);
}
