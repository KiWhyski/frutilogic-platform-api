using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the ProductName value object.
/// </summary>
public class ProductNameJsonConverter : JsonConverter<ProductName>
{
    public override ProductName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return new ProductName(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, ProductName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue());
    }
}
