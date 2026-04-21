using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     This class provides a custom serializer for the Money value object to be used in MongoDB.
/// </summary>
public class MoneySerializer : IBsonSerializer<Money>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Money value)
    {
        var stringValue = $"{value.GetAmount()} {value.GetCurrencyCode()}";
        context.Writer.WriteString(stringValue);
    }

    public Money Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        var parts = value.Split(' ');
        var amount = decimal.Parse(parts[0]);
        var currencyCode = parts[1];
        return new Money(amount, new Currency(currencyCode));
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (Money)value);
    
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(Money);
}
