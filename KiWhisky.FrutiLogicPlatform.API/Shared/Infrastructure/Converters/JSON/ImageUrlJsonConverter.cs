using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Converters.JSON;

/// <summary>
///     This class is a custom JSON converter for the ImageUrl value object.
/// </summary>
public class ImageUrlJsonConverter : JsonConverter<ImageUrl>
{
    public override ImageUrl? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return new ImageUrl(value ?? "");
    }

    public override void Write(Utf8JsonWriter writer, ImageUrl value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
