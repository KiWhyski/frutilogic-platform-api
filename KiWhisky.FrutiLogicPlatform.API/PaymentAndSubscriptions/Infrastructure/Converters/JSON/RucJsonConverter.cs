using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the Ruc value object to JSON string representation.
/// </summary>
public class RucJsonConverter : JsonConverter<Ruc>
{
    public override Ruc? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return new Ruc(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, Ruc value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
