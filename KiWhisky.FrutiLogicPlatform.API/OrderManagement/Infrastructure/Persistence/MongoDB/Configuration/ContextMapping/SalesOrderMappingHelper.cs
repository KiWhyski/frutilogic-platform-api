using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;

public static class SalesOrderMappingHelper
{
    public static void RegisterSalesOrderManagementMappings()
    {
        SerializerRegistrationHelper.TryRegisterSerializer(new EnumSerializer<ESalesOrderStatuses>(BsonType.String));
        SerializerRegistrationHelper.TryRegisterSerializer(new EnumSerializer<DeliveryProposalStatus>(BsonType.String));
        SalesOrderMapping.ConfigureBsonMapping();
    }
}
