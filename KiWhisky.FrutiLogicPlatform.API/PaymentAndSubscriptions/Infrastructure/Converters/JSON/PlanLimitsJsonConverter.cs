using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Converters.JSON;

/// <summary>
///     Converter for the PlanLimits value object to JSON string representation.
/// </summary>
public class PlanLimitsJsonConverter : JsonConverter<PlanLimits>
{
    public override PlanLimits? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        reader.Read();
        var maxUsers = reader.GetInt32();

        reader.Read();
        var maxWarehouses = reader.GetInt32();

        reader.Read();
        var maxProducts = reader.GetInt32();

        reader.Read();
        return new PlanLimits(maxUsers, maxWarehouses, maxProducts);
    }

    public override void Write(Utf8JsonWriter writer, PlanLimits value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.MaxUsers);
        writer.WriteNumberValue(value.MaxWarehouses);
        writer.WriteNumberValue(value.MaxProducts);
        writer.WriteEndArray();
    }
}
