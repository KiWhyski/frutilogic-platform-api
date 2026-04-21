using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     This class provides a custom serializer for the InventoryId value object to be used in MongoDB.
/// </summary>
public class InventoryIdSerializer : IBsonSerializer<InventoryId>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, InventoryId value)
    {
        context.Writer.WriteString(value.GetId);
    }

    public InventoryId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new InventoryId((value) ?? throw new InvalidOperationException());
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (InventoryId)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(InventoryId);
}
