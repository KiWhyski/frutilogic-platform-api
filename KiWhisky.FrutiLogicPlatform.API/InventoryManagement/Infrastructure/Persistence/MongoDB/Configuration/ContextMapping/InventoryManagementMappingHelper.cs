using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using AccountIdSerializer = KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers.AccountIdSerializer;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;

/// <summary>
///     Static helper class for registering MongoDB mappings specific to Inventory Management.
/// </summary>
public static class InventoryManagementMappingHelper
{
    /// <summary>
    ///     Static method to register all Inventory Management related MongoDB mappings.
    /// </summary>
    public static void RegisterInventoryManagementMappings()
    {
        //
        // Use of Product-related Value Objects
        //
        
        // Use of EBrandNames Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new EnumSerializer<EBrandNames>(BsonType.String));
        
        // Use of EProductTypes Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new EnumSerializer<EProductTypes>(BsonType.String));
        
        // Use of ProductContent Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new ProductContentSerializer());
        
        // Use of ProductMinimumStock Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new ProductMinimumStockSerializer());
        
        // Use of ProductName Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new ProductNameSerializer());

        //
        // Register CareGuide mapping and serializers
        //
        CareGuideMapping.ConfigureBsonMapping();
        
        //
        // Use of Warehouse-related Value Object
        //
        
        // Use of WarehouseCapacity Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new WarehouseCapacitySerializer());
        
        // Use of WarehouseAddress Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new WarehouseAddressSerializer());
        
        //
        // Use of Inventory-related Value Object
        //
        
        // Use of ProductExpirationDate Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new ProductExpirationDateSerializer());
        
        // Use of ProductStock Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new ProductStockSerializer());
        
        // Use of EProductStates Value Object serializer
        SerializerRegistrationHelper.TryRegisterSerializer(new EnumSerializer<EProductStates>(BsonType.String));
    }
}

