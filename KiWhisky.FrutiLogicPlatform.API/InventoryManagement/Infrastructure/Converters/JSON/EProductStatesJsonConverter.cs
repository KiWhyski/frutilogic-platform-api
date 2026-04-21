using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using Microsoft.OpenApi.Extensions;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for EProductStates enum to JSON string representation.
/// </summary>
public class EProductStatesJsonConverter : JsonConverter<EProductStates>
{
    public override EProductStates Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return Enum.Parse<EProductStates>(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, EProductStates value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetDisplayName());
    }
}
