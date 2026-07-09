using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
/// Tolerant serializer for product types. Maps legacy beverage enum values to Others
/// so existing MongoDB documents keep loading after the fruit rebrand.
/// </summary>
public class EProductTypesSerializer : SerializerBase<EProductTypes>
{
    private static readonly HashSet<string> LegacyBeverageTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "Sodas", "Wines", "Rums", "Whiskeys", "Beers", "Piscos", "Tequilas",
        "Vodkas", "Gins", "Brandies", "Cognacs", "Champagnes", "Ciders",
        "Liqueurs", "Spirits", "Beverages"
    };

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, EProductTypes value)
    {
        context.Writer.WriteString(value.ToString());
    }

    public override EProductTypes Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonType = context.Reader.GetCurrentBsonType();
        string raw;

        switch (bsonType)
        {
            case BsonType.String:
                raw = context.Reader.ReadString();
                break;
            case BsonType.Int32:
                raw = context.Reader.ReadInt32().ToString();
                break;
            case BsonType.Null:
                context.Reader.ReadNull();
                return EProductTypes.Others;
            default:
                context.Reader.SkipValue();
                return EProductTypes.Others;
        }

        if (string.IsNullOrWhiteSpace(raw))
            return EProductTypes.Others;

        if (Enum.TryParse<EProductTypes>(raw, ignoreCase: true, out var parsed))
            return parsed;

        if (LegacyBeverageTypes.Contains(raw))
            return EProductTypes.Others;

        return EProductTypes.Others;
    }
}
