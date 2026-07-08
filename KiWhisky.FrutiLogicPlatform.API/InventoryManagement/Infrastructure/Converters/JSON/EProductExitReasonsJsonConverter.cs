using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Extensions;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Json converter for EProductExitReasons enum.
/// </summary>
public class EProductExitReasonsJsonConverter : JsonConverter<EProductExitReasons>
{
    public override EProductExitReasons Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return Enum.Parse<EProductExitReasons>(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, EProductExitReasons value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetDisplayName());
    }
}
