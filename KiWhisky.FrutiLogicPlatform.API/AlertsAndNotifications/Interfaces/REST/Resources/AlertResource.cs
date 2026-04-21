namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.REST.Resources
{
    /// <summary>
    /// This record defines the alert resource.
    /// </summary>
    public record AlertResource(string Id, string Title, string Message, string Severity, string Type, string AccountId, string InventoryId);
}

