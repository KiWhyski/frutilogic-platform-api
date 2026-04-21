using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the EAccountStatuses enum to JSON string representation.
/// </summary>
public class EAccountStatusesJsonConverter : JsonConverter<EAccountStatuses>
{
    public override EAccountStatuses Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return Enum.Parse<EAccountStatuses>(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, EAccountStatuses value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
