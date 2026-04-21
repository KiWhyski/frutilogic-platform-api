using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Converters.JSON;
/// <summary>
///     This class is a custom JSON converter for the AccountId value object.
/// </summary>
public class AccountIdJsonConverter : JsonConverter<AccountId>
{
    public override AccountId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }
        return AccountId.Create(value);
    }
    
    public override void Write(Utf8JsonWriter writer, AccountId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetId);
    }
}
