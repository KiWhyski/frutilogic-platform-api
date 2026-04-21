using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Serializer for the WarehouseCapacity value object to be used in MongoDB.
/// </summary>
public class WarehouseCapacitySerializer : IBsonSerializer<WarehouseCapacity>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, WarehouseCapacity value)
    {
        context.Writer.WriteDouble(value.GetValue());
    }

    public WarehouseCapacity Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadDouble();
        return new WarehouseCapacity(value);
    }
    
    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (WarehouseCapacity)value);
    
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);
    
    public Type ValueType => typeof(WarehouseCapacity);
}
