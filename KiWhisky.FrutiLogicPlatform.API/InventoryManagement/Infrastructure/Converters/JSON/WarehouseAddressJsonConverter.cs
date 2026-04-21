using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     Class for converting WarehouseAddress value object to and from JSON.
/// </summary>
public class WarehouseAddressJsonConverter : JsonConverter<WarehouseAddress>
{
    public override WarehouseAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        var street = "";
        var city = "";
        var district = "";
        var postalCode = "";
        var country = "";

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException();

            var propertyName = reader.GetString();
            reader.Read(); // Move to value

            switch (propertyName)
            {
                case "Street":
                    street = reader.GetString() ?? "";
                    break;
                case "City":
                    city = reader.GetString() ?? "";
                    break;
                case "District":
                    district = reader.GetString() ?? "";
                    break;
                case "PostalCode":
                    postalCode = reader.GetString() ?? "";
                    break;
                case "Country":
                    country = reader.GetString() ?? "";
                    break;
                default:
                    reader.Skip();
                    break;
            }
        }
        return new WarehouseAddress(street, city, district, postalCode, country);
    }

    public override void Write(Utf8JsonWriter writer, WarehouseAddress value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("Street", value.Street);
        writer.WriteString("City", value.City);
        writer.WriteString("District", value.District);
        writer.WriteString("PostalCode", value.PostalCode);
        writer.WriteString("Country", value.Country);

        writer.WriteEndObject();
    }
}
