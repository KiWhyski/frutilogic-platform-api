using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Serializer for the PlanLimits value object to be used in MongoDB.
/// </summary>
public class PlanLimitsSerializer : IBsonSerializer<PlanLimits>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PlanLimits value)
    {
        context.Writer.WriteStartArray();
        context.Writer.WriteInt32(value.MaxUsers);
        context.Writer.WriteInt32(value.MaxWarehouses);
        context.Writer.WriteInt32(value.MaxProducts);
        context.Writer.WriteEndArray();
    }

    public PlanLimits Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        context.Reader.ReadStartDocument();
        var maxUsers = context.Reader.ReadInt32();
        var maxWarehouses = context.Reader.ReadInt32();
        var maxProducts = context.Reader.ReadInt32();
        context.Reader.ReadEndDocument();
        return new PlanLimits(maxUsers, maxWarehouses, maxProducts);
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (PlanLimits)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(PlanLimits);
}
