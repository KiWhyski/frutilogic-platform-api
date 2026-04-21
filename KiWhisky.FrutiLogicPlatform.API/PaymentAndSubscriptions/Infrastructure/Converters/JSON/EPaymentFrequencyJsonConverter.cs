using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the EPaymentFrequency enum to JSON string representation.
/// </summary>
public class EPaymentFrequencyJsonConverter : JsonConverter<EPaymentFrequency>
{
    public override EPaymentFrequency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return Enum.Parse<EPaymentFrequency>(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, EPaymentFrequency value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
