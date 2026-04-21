using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;

/// <summary>
///     Static helper class for registering MongoDB mappings specific to Profile Management.
/// </summary>
public static class ProfileManagementMappingHelper
{
    /// <summary>
    ///     Static method to register all Profile Management related MongoDB mappings.
    /// </summary>
    public static void RegisterProfileManagementMappings()
    {
        //
        // Use of Profile-related Value Objects
        //

        // PersonName Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new PersonNameSerializer());

        // PersonContactNumber Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new PersonContactNumberSerializer());
        
        // ProfileRole Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new EnumSerializer<EProfileRole>(BsonType.String));
        
    }
}
