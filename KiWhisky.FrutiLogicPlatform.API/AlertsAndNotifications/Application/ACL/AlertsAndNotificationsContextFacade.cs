using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.ACL;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Application.ACL
{
    /// <summary>
    /// This class serves as a facade for the Alerts and Notifications context, providing methods to create alerts for other contexts.
    /// </summary>
    /// <param name="alertCommandService">
    /// The command service for handling alert operations.
    /// </param>
    /// <param name="alertQueryService">
    /// The query service for retrieving alert information.
    /// </param>
    public class AlertsAndNotificationsContextFacade(
        IAlertCommandService alertCommandService,
        IAlertQueryService alertQueryService
        ) : IAlertsAndNotificationsContextFacade
    {
        /// <summary>
        /// Creates a new alert.
        /// </summary>
        /// <param name="title">The title of the alert.</param>
        /// <param name="message">The message of the alert.</param>
        /// <param name="severity">The severity of the alert.</param>
        /// <param name="type">The type of the alert.</param>
        /// <param name="inventoryId">The ID of the inventory associated with the alert.</param>
        /// <param name="profileId">The ID of the profile associated with the alert.</param>
        /// <returns>The ID of the created alert.</returns>
        public async Task<string> CreateAlert(string title, string message, string severity, string type,
            string accountId, string inventoryId, AlertDetails? details = null, string? idempotencyKey = null)
        {
            if (string.IsNullOrEmpty(inventoryId))
                throw new ArgumentException("Inventory ID cannot be null or empty", nameof(inventoryId));
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentException("Account ID cannot be null or empty", nameof(accountId));

            var targetAccountId = new AccountId(accountId);
            var targetInventoryId = new InventoryId(inventoryId);
            var createAlertCommand = new CreateAlertCommand(title, message, severity, type, targetAccountId,
                targetInventoryId, details, idempotencyKey);
            var alert = await alertCommandService.Handle(createAlertCommand);
            return alert?.Id.ToString() ?? string.Empty;
        }
    }
}

