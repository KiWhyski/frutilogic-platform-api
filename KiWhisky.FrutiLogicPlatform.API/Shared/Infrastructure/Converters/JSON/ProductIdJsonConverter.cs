using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Converters.JSON;

/// <summary>
///     The JSON converter for the ProductId Value Object.
/// </summary>
public class ProductIdJsonConverter : JsonConverter<ProductId>
{
    public override ProductId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return new ProductId(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, ProductId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetId);
    }
}
