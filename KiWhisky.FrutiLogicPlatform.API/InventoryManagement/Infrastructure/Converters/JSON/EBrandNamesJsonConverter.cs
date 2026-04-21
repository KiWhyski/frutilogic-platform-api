using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using Microsoft.OpenApi.Extensions;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for EBrandNames enum to JSON string representation.
/// </summary>
public class EBrandNamesJsonConverter : JsonConverter<EBrandNames>
{
    public override EBrandNames Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {   
        var value = reader.GetString();
        return Enum.Parse<EBrandNames>(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, EBrandNames value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetDisplayName());
    }
}
