using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the ProductExpirationDate value object to and from JSON.
/// </summary>
public class ProductExpirationDateJsonConverter : JsonConverter<ProductExpirationDate>
{
    public override ProductExpirationDate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetDateTime();
        var dateTimeValue = DateOnly.FromDateTime(value);
        return new ProductExpirationDate(dateTimeValue);
    }

    public override void Write(Utf8JsonWriter writer, ProductExpirationDate value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
