using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     The BSON serializer for the <see cref="CatalogId" /> value object.
/// </summary>
public class CatalogIdSerializer : IBsonSerializer<CatalogId>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, CatalogId value)
    {
        context.Writer.WriteString(value.GetId());
    }

    public CatalogId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new CatalogId((value) ?? throw new InvalidOperationException());
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (CatalogId)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(CatalogId);
}
