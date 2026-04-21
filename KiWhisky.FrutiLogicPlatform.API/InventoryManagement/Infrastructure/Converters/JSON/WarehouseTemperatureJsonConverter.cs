using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Class for converting WarehouseTemperature value object to and from JSON.
/// </summary>
public class WarehouseTemperatureJsonConverter : JsonConverter<WarehouseTemperature>
{
    public override WarehouseTemperature? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        decimal minTemperature = 0;
        decimal maxTemperature = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException();

            var propertyName = reader.GetString();
            reader.Read();

            switch (propertyName)
            {
                case "MinTemperature":
                    minTemperature = reader.GetDecimal();
                    break;
                case "MaxTemperature":
                    maxTemperature = reader.GetDecimal();
                    break;
                default:
                    reader.Skip();
                    break;
            }
        }
        return new WarehouseTemperature(minTemperature, maxTemperature);
    }

    public override void Write(Utf8JsonWriter writer, WarehouseTemperature value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteNumber("MinTemperature", value.GetMinTemperature());
        writer.WriteNumber("MaxTemperature", value.GetMaxTemperature());

        writer.WriteEndObject();
    }
}
