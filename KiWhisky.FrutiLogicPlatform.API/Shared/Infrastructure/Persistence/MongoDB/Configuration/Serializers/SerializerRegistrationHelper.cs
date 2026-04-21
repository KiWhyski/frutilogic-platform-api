using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

/// <summary>
///     Helper class to register MongoDB serializers safely.
///     It checks if a serializer is already registered for a type before attempting to register a new
/// </summary>
public static class SerializerRegistrationHelper
{
    /// <summary>
    ///     Method to try registering a MongoDB serializer for a specific type T.
    /// </summary>
    public static void TryRegisterSerializer<T>(IBsonSerializer<T> serializer)
    {
        var type = typeof(T);
        var registry = BsonSerializer.SerializerRegistry;
        var currentSerializer = registry.GetSerializer<T>();
        
        if (currentSerializer.GetType() == serializer.GetType()) return;
        
        try
        {
            BsonSerializer.RegisterSerializer(serializer);
            Console.WriteLine($"Registering serializer for {typeof(T).Name}");
        }
        catch (BsonSerializationException ex)
        {
            if (ex.Message.Contains("There is already a serializer registered"))
            {
                Console.WriteLine($"There is already a registered serializer for {typeof(T).Name}, ignoring it.");
            }
        }
    }
}
