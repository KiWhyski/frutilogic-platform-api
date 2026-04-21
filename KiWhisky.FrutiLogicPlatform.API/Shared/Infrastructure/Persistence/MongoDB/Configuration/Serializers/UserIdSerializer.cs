using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     This class provides a custom serializer for the UserId value object to be used in MongoDB.
/// </summary>
public class UserIdSerializer : IBsonSerializer<OwnerUserId>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, OwnerUserId value)
    {
        context.Writer.WriteString(value.GetId);
    }

    public OwnerUserId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new OwnerUserId(value ?? throw new InvalidOperationException());
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (OwnerUserId)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(OwnerUserId);
}
