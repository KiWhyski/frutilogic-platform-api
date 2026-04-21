using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     BSON serializer for PersonContactNumber value object.
/// </summary>
public class PersonContactNumberSerializer : SerializerBase<PersonContactNumber>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PersonContactNumber value)
    {
        context.Writer.WriteString(value.PhoneNumber);
    }

    public override PersonContactNumber Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var phoneNumber = context.Reader.ReadString();
        return new PersonContactNumber(phoneNumber);
    }
}
