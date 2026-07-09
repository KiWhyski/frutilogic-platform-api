namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

/// <summary>
/// Structured data used by clients to render an alert without parsing its message.
/// </summary>
public record AlertDetails(
    string? ProductId = null,
    string? ProductName = null,
    string? WarehouseId = null,
    string? WarehouseName = null,
    int? CurrentStock = null,
    int? MinimumStock = null,
    string? ExpirationDate = null,
    int? DaysUntilExpiration = null);
