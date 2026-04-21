using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Converters.JSON;

/// <summary>
///     This class is a custom JSON converter for the Money value object.
///     It handles the serialization and deserialization of Money objects to and from JSON format.
/// </summary>
public class MoneyJsonConverter : JsonConverter<Money>
{
    public override Money Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var amount = root.GetProperty("amount").GetDecimal();
        var currencyCode = root.GetProperty("currency").GetProperty("code").GetString();

        return new Money(amount, new Currency(currencyCode ?? throw new InvalidOperationException()));
    }

    public override void Write(Utf8JsonWriter writer, Money value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteNumber("amount", value.GetAmount());

        writer.WritePropertyName("currency");
        writer.WriteStartObject();
        writer.WriteString("code", value.GetCurrencyCode());
        writer.WriteEndObject();

        writer.WriteEndObject();
    }
}
