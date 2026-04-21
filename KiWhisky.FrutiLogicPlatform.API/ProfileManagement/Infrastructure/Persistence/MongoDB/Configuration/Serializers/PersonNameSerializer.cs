using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     BSON serializer for PersonName value object.
/// </summary>
public class PersonNameSerializer : SerializerBase<PersonName>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PersonName value)
    {
        context.Writer.WriteStartDocument();
        context.Writer.WriteName(nameof(PersonName.FirstName));
        context.Writer.WriteString(value.FirstName);
        context.Writer.WriteName(nameof(PersonName.LastName));
        context.Writer.WriteString(value.LastName);
        context.Writer.WriteEndDocument();
    }

    public override PersonName Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        context.Reader.ReadStartDocument();

        string? firstName = null;
        string? lastName = null;

        while (context.Reader.ReadBsonType() != BsonType.EndOfDocument)
        {
            var fieldName = context.Reader.ReadName();
            switch (fieldName)
            {
                case nameof(PersonName.FirstName):
                    firstName = context.Reader.ReadString();
                    break;
                case nameof(PersonName.LastName):
                    lastName = context.Reader.ReadString();
                    break;
                default:
                    context.Reader.SkipValue();
                    break;
            }
        }

        context.Reader.ReadEndDocument();

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new BsonSerializationException("Invalid BSON for PersonName. Both FirstName and LastName are required.");

        return new PersonName(firstName, lastName);
    }
}
