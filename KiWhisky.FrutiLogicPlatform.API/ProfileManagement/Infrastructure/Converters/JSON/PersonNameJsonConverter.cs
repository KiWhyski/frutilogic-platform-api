using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the PersonName value object to and from JSON.
/// </summary>
public class PersonNameJsonConverter : JsonConverter<PersonName>
{
    public override PersonName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token for PersonName.");

        string? firstName = null;
        string? lastName = null;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType != JsonTokenType.PropertyName)
                continue;

            var propertyName = reader.GetString();
            reader.Read();

            switch (propertyName)
            {
                case nameof(PersonName.FirstName):
                    firstName = reader.GetString();
                    break;
                case nameof(PersonName.LastName):
                    lastName = reader.GetString();
                    break;
            }
        }

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new JsonException("Invalid PersonName JSON. Both FirstName and LastName are required.");

        return new PersonName(firstName, lastName);
    }

    public override void Write(Utf8JsonWriter writer, PersonName value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString(nameof(PersonName.FirstName), value.FirstName);
        writer.WriteString(nameof(PersonName.LastName), value.LastName);
        writer.WriteEndObject();
    }
}
