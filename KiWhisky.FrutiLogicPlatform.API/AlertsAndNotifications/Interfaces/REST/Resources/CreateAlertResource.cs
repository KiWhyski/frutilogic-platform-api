namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.REST.Resources
{
    /// <summary>
    /// This record defines the resource for creating a new alert.
    /// </summary>
    public record CreateAlertResource(string Title, string Message, string Severity, string Type, string AccountId, string InventoryId);
}

