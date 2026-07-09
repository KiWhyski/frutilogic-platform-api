using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;
using BsonType = global::MongoDB.Bson.BsonType;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     This class provides a custom serializer for the AccountId value object to be used in MongoDB.
/// </summary>
public class AccountIdSerializer : IBsonSerializer<AccountId>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, AccountId value)
    {
        context.Writer.WriteString(value.GetId);
    }

    public AccountId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonType = context.Reader.GetCurrentBsonType();
        if (bsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            // Nullable AccountId? fields (e.g. Product.SupplierId) may be stored as null.
            return AccountId.Create("000000000000000000000000");
        }

        var value = context.Reader.ReadString();
        if (string.IsNullOrEmpty(value))
        {
            return AccountId.Create("000000000000000000000000");
        }
        return AccountId.Create(value);
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (AccountId)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(AccountId);
}
