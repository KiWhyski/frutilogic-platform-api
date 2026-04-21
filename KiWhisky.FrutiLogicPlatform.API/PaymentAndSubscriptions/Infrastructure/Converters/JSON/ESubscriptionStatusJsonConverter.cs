using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the ESubscriptionStatus enum to JSON string representation.
/// </summary>
public class ESubscriptionStatusJsonConverter : JsonConverter<ESubscriptionStatus>
{
    public override ESubscriptionStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return Enum.Parse<ESubscriptionStatus>(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, ESubscriptionStatus value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
