using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Serializer for the BusinessName value object to be used in MongoDB.
/// </summary>
public class BusinessNameSerializer : IBsonSerializer<BusinessName>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BusinessName value)
        => context.Writer.WriteString(value.Value);

    public BusinessName Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new BusinessName(value);
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (BusinessName)value);
    
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(BusinessName);
}
