using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.REST.Transform
{
    /// <summary>
    /// This class defines the assembler for converting an alert entity to a resource.
    /// </summary>
    public static class AlertResourceFromEntityAssembler
    {
        public static AlertResource ToResourceFromEntity(Alert entity)
        {
            return new AlertResource(
                entity.Id.ToString(),
                entity.Title,
                entity.Message,
                entity.Severity.ToString(),
                entity.Type.ToString(),
                entity.AccountId.GetId,
                entity.InventoryId.GetId,
                entity.GeneratedAt,
                entity.Details
            );
        }
    }
}

