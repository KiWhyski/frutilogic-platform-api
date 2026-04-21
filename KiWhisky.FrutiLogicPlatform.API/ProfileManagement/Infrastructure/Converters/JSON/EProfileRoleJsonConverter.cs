using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;
using Newtonsoft.Json;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the EProfileRole value object to and from JSON.
/// </summary>
public class EProfileRoleJsonConverter : JsonConverter<EProfileRole>
{
    public override void WriteJson(JsonWriter writer, EProfileRole value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override EProfileRole ReadJson(JsonReader reader, Type objectType, EProfileRole existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var value = reader.Value?.ToString();
        return Enum.Parse<EProfileRole>(value ?? throw new InvalidOperationException());
    }
}
