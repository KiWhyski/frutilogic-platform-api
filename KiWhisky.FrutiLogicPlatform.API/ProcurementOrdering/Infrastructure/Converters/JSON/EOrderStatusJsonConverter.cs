using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Infrastructure.Converters.JSON;

public class EOrderStatusJsonConverter : JsonConverter<EOrderStatus>
{
    public override EOrderStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var enumString = reader.GetString();
        return Enum.Parse<EOrderStatus>(enumString!, true);
    }

    public override void Write(Utf8JsonWriter writer, EOrderStatus value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
