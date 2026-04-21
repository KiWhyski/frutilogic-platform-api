using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     This class provides a custom serializer for the ImageUrl value object to be used in MongoDB.
/// </summary>
public class ImageUrlSerializer : IBsonSerializer<ImageUrl>
{
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ImageUrl value)
    {
        context.Writer.WriteString(value.GetValue());
    }

    public ImageUrl Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new ImageUrl(value ?? "");
    }

    void IBsonSerializer.Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, (ImageUrl)value);

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public Type ValueType => typeof(ImageUrl);
}
