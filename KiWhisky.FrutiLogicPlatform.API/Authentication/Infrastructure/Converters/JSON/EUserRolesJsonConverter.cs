using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Infrastructure.Converters.JSON;

/// <summary>
///     
/// </summary>
public class EUserRolesJsonConverter : JsonConverter<EUserRoles> 
{
    public override EUserRoles Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return Enum.Parse<EUserRoles>(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, EUserRoles value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
