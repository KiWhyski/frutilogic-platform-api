using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the EAccountRole enum to JSON string representation.
/// </summary>
public class EAccountRoleJsonConverter : JsonConverter<EAccountRole>
{
    public override EAccountRole Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return Enum.Parse<EAccountRole>(value ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, EAccountRole value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
