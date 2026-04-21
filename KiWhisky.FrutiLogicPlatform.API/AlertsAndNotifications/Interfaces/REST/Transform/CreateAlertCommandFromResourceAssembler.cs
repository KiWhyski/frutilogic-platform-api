using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.REST.Transform
{
    /// <summary>
    /// This static class is responsible for transforming a CreateAlertResource into a CreateAlertCommand.
    /// </summary>
    public static class CreateAlertCommandFromResourceAssembler
    {
        public static CreateAlertCommand ToCommandFromResource(CreateAlertResource resource)
        {
            var accountId = new AccountId(resource.AccountId);
            var inventoryId = new InventoryId(resource.InventoryId);
            return new CreateAlertCommand(resource.Title, resource.Message, resource.Severity, resource.Type, accountId, inventoryId);
        }
    }
}

