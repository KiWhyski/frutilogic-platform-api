using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Commands
{
    public record CreateAlertCommand(
        string Title,
        string Message,
        string Severity,
        string Type,
        AccountId AccountId,
        InventoryId InventoryId,
        AlertDetails? Details = null,
        string? IdempotencyKey = null);
}

