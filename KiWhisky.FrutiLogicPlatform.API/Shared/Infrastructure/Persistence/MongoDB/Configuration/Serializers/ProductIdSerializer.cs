using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     The custom serializer for the ProductId value object to be used in MongoDB.
/// </summary>
public class ProductIdSerializer : IBsonSerializer<ProductId>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ProductId value)
    {
        context.Writer.WriteString(value.GetId);
    }

    public ProductId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new ProductId(value ?? throw new InvalidOperationException());
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (ProductId)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(ProductId);
}
