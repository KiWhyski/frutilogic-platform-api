using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Serializer for the ProductContent value object to be used in MongoDB.
/// </summary>
public class ProductContentSerializer : IBsonSerializer<ProductContent>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ProductContent value)
    {
        context.Writer.WriteDecimal128(value.GetValue());
    }

    public ProductContent Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadDecimal128();
        var decimalValue = Decimal128.ToDecimal(value); 
        return new ProductContent(decimalValue);
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (ProductContent)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);
    
    public Type ValueType => typeof(ProductContent);
}
