using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.ACL;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Application.ACL;

/// <summary>
///     This service is used to create alerts and notifications in the Alerts and Notifications context.
///     The information will be sent from the Inventory Management context to the Alerts and Notifications context.
/// </summary>
/// <param name="alertsAndNotificationsContextFacade">
///     The facade for the Alerts and Notifications context.
/// </param>
public class ExternalAlertsAndNotificationsService(IAlertsAndNotificationsContextFacade alertsAndNotificationsContextFacade)
{
    /// <summary>
    ///     The method is used to create an alert in the Alerts and Notifications context.
    /// </summary>
    public Task<string> CreateAlertAsync(string title, string message, string severity, string type, string accountId,
        string inventoryId, AlertDetails? details = null, string? idempotencyKey = null)
    {
        return alertsAndNotificationsContextFacade.CreateAlert(
            title, 
            message, 
            severity, 
            type, 
            accountId, 
            inventoryId,
            details,
            idempotencyKey
            );
    }
}
