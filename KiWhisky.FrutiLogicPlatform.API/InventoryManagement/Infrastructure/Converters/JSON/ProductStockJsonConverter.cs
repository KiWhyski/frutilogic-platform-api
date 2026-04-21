using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the ProductStock value object to and from JSON.
/// </summary>
public class ProductStockJsonConverter : JsonConverter<ProductStock>
{
    public override ProductStock? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetInt32();
        return new ProductStock(value);
    }

    public override void Write(Utf8JsonWriter writer, ProductStock value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.GetValue);
    }
}
