using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Serializer for the BusinessEmail value object to be used in MongoDB.
/// </summary>
public class BusinessEmailSerializer : IBsonSerializer<BusinessEmail>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BusinessEmail value)
        => context.Writer.WriteString(value.Value);
    
    public BusinessEmail Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new BusinessEmail(value);
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (BusinessEmail)value);
    
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(BusinessEmail);
}
