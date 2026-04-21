using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Queries
{
    public record GetAllAlertsByInventoryIdQuery(InventoryId InventoryId);
}

