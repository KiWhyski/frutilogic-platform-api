using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;

/// <summary>
///     This class provides helper methods for MongoDB mapping configurations.
///     It allows the mapping for value objects that are used inside a class.
/// </summary>
public static class SharedMappingHelper
{
    /// <summary>
    ///     This method registers all shared MongoDB mappings.
    ///     It should be called once during the application startup.
    /// </summary>
    public static void RegisterSharedMappings()
    {
        // Use of AccountId Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new AccountIdSerializer());
        
        // Use of UserId Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new UserIdSerializer());
        
        // Use of UserId Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new ProductIdSerializer());
        
        // Use of InventoryId Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new InventoryIdSerializer());
        
        // Use of PurchaseOrderId Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new PurchaseOrderIdSerializer());
        
        // Use of PurchaseOrderId Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new CatalogIdSerializer());
        
        // Use of Email/Username Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new EmailSerializer());
        
        // Use of ImageUrl Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new ImageUrlSerializer());
        
        // Use of Money Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new MoneySerializer());
    }
}
