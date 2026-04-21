using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the BusinessName value object to and from JSON.
/// </summary>
public class BusinessNameJsonConverter : JsonConverter<BusinessName>
{
    public override BusinessName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return new BusinessName(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, BusinessName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
