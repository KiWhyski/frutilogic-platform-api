using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Queries;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Services
{
    public interface IAlertQueryService
    {
        Task<Alert?> Handle(GetAlertByIdQuery query);
    
        Task<IEnumerable<Alert>> Handle(GetAllAlertsByInventoryIdQuery query);
        Task<IEnumerable<Alert>> Handle(GetAllAlertsByAccountIdQuery query);
        Task<int> Handle(GenerateExpirationAlertsQuery query);
    }
}

