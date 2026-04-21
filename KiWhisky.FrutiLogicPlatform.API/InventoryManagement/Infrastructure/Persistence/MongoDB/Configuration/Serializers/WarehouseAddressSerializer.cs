using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Serializer for the WarehouseAddress value object to be used in MongoDB.
/// </summary>
public class WarehouseAddressSerializer : IBsonSerializer<WarehouseAddress>
{
    public WarehouseAddress Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var reader = context.Reader;

        reader.ReadStartDocument();

        var street = string.Empty;
        var city = string.Empty;
        var district = string.Empty;
        var postalCode = string.Empty;
        var country = string.Empty;

        while (reader.ReadBsonType() != BsonType.EndOfDocument)
        {
            var name = reader.ReadName();
            switch (name)
            {
                case nameof(WarehouseAddress.Street):
                    street = reader.ReadString();
                    break;
                case nameof(WarehouseAddress.City):
                    city = reader.ReadString();
                    break;
                case nameof(WarehouseAddress.District):
                    district = reader.ReadString();
                    break;
                case nameof(WarehouseAddress.PostalCode):
                    postalCode = reader.ReadString();
                    break;
                case nameof(WarehouseAddress.Country):
                    country = reader.ReadString();
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndDocument();

        return new WarehouseAddress(street, city, district, postalCode, country);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, WarehouseAddress value)
    {
        var writer = context.Writer;

        writer.WriteStartDocument();

        writer.WriteName(nameof(WarehouseAddress.Street));
        writer.WriteString(value.Street);

        writer.WriteName(nameof(WarehouseAddress.City));
        writer.WriteString(value.City);

        writer.WriteName(nameof(WarehouseAddress.District));
        writer.WriteString(value.District);

        writer.WriteName(nameof(WarehouseAddress.PostalCode));
        writer.WriteString(value.PostalCode);

        writer.WriteName(nameof(WarehouseAddress.Country));
        writer.WriteString(value.Country);

        writer.WriteEndDocument();
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (WarehouseAddress)value);

    public Type ValueType => typeof(WarehouseAddress);
}
