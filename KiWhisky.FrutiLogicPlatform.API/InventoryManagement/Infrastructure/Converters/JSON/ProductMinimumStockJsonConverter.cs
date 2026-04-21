using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the ProductMinimumStock value object to and from JSON.
/// </summary>
public class ProductMinimumStockJsonConverter : JsonConverter<ProductMinimumStock>
{
    public override ProductMinimumStock? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetInt32();
        return new ProductMinimumStock(value);
    }

    public override void Write(Utf8JsonWriter writer, ProductMinimumStock value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.GetValue());
    }
}
