using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the PersonContactNumber value object to and from JSON.
/// </summary>
public class PersonContactNumberJsonConverter : JsonConverter<PersonContactNumber>
{
    public override PersonContactNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var phoneNumber = reader.GetString();
        if (phoneNumber is null)
            throw new JsonException("Phone number cannot be null.");

        return new PersonContactNumber(phoneNumber);
    }

    public override void Write(Utf8JsonWriter writer, PersonContactNumber value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.PhoneNumber);
    }
}
