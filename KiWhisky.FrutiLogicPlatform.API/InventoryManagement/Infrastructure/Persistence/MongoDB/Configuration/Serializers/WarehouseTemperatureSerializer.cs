using System.Globalization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Serializer for the WarehouseTemperature value object to be used in MongoDB.
/// </summary>
public class WarehouseTemperatureSerializer : IBsonSerializer<WarehouseTemperature>
{
    public WarehouseTemperature Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var reader = context.Reader;
        reader.ReadStartDocument();

        decimal minTemperature = 0;
        decimal maxTemperature = 0;

        while (reader.ReadBsonType() != BsonType.EndOfDocument)
        {
            var name = reader.ReadName();
            switch (name)
            {
                case "MinTemperature":
                    minTemperature = decimal.Parse(reader.ReadDecimal128().ToString(), CultureInfo.InvariantCulture);
                    break;
                case "MaxTemperature":
                    maxTemperature = decimal.Parse(reader.ReadDecimal128().ToString(), CultureInfo.InvariantCulture);
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }
        reader.ReadEndDocument();
        return new WarehouseTemperature(minTemperature, maxTemperature);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, WarehouseTemperature value)
    {
        var writer = context.Writer;

        writer.WriteStartDocument();

        writer.WriteName("MinTemperature");
        writer.WriteDecimal128(value.GetMinTemperature());

        writer.WriteName("MaxTemperature");
        writer.WriteDecimal128(value.GetMaxTemperature());

        writer.WriteEndDocument();
    }
    
    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (WarehouseTemperature)value);
    
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(WarehouseTemperature);
}
