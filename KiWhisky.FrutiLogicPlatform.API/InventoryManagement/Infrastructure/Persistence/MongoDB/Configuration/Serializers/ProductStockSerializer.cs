using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Serializer for the ProductStock value object to be used in MongoDB.
/// </summary>
public class ProductStockSerializer : IBsonSerializer<ProductStock>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ProductStock value)
    {
        context.Writer.WriteInt32(value.GetValue);
    }

    public ProductStock Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new ProductStock(value);
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (ProductStock)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);
    
    public Type ValueType => typeof(ProductStock);
}
