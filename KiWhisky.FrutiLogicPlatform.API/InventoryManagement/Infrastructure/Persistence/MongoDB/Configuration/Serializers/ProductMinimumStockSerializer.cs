using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Serializer for the ProductMinimumStock value object to be used in MongoDB.
/// </summary>
public class ProductMinimumStockSerializer : IBsonSerializer<ProductMinimumStock>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ProductMinimumStock value)
    {
        context.Writer.WriteInt32(value.GetValue());
    }

    public ProductMinimumStock Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new ProductMinimumStock(value);
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (ProductMinimumStock)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);
    
    public Type ValueType => typeof(ProductMinimumStock);
}
