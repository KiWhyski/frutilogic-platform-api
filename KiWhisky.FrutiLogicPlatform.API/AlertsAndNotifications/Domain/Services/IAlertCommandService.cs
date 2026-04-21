using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Commands;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Services;

public interface IAlertCommandService
{
    Task<Alert?> Handle(CreateAlertCommand command);
}
