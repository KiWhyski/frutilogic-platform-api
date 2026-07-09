using KiWhisky.FrutiLogicPlatform.API.Authentication.Infrastructure.Persistence.MongoDB.Configuration;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;
using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Events;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;
using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.ContextMapping;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;

/// <summary>
///     This class groups all MongoDB mapping helpers for different bounded contexts.
/// </summary>
public static class GlobalMongoMappingHelper
{
    private static bool _initialized = false;

    /// <summary>
    ///     Registers all MongoDB mappings for all bounded contexts.
    /// </summary>
    public static void RegisterAllBoundedContextMappings()
    {
        if (_initialized) return;
        
        // Shared Bounded Context
        Console.WriteLine("Registering Shared Mappings...");
        SharedMappingHelper.RegisterSharedMappings();
        Console.WriteLine("Shared Mappings Registered!");
        
        // IAM Bounded Context
        Console.WriteLine("Registering Authentication Mappings...");
        AuthenticationMappingHelper.RegisterAuthenticationMappings();
        Console.WriteLine("Authentication Mappings Registered!");
        
        // Inventory Bounded Context
        Console.WriteLine("Registering Inventory Management Mappings...");
        InventoryManagementMappingHelper.RegisterInventoryManagementMappings();
        Console.WriteLine("Inventory Management Mappings Registered!");
        
        // Order Management Bounded Context
        Console.WriteLine("Registering Order Management Mappings...");
        SalesOrderMappingHelper.RegisterSalesOrderManagementMappings();
        Console.WriteLine("Order Management Mappings Registered!");
        
        // Procurement Ordering Bounded Context
        Console.WriteLine("Registering Procurement Ordering Mappings...");
        ProcurementOrderingMappingHelper.RegisterProcurementOrderingMappings();
        Console.WriteLine("Procurement Ordering Mappings Registered!");
        
        // Subscription Bounded Context
        Console.WriteLine("Registering Payment and Subscriptions Mappings...");
        PaymentAndSubscriptionsMappingHelper.RegisterPaymentAndSubscriptionsMappings();
        Console.WriteLine("Payment and Subscriptions Mappings Registered!");
        
        // Alerts Bounded Context
        
        // Profiles Bounded Context
        Console.WriteLine("Registering Profile Management Mappings...");
        ProfileManagementMappingHelper.RegisterProfileManagementMappings();
        Console.WriteLine("Profile Management Mappings Registered!");
        
        _initialized = true;
    }
}
