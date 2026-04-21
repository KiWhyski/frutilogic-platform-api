using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Repositories;

public interface IAlertRepository: IBaseRepository<Alert>
{
    Task<List<Alert>> GetAllAlertsByAccountId(string accountId);
    Task<List<Alert>> GetAlertsByInventoryId(string inventoryId);
    Task<Alert> GenerateAlert(string accountId, string type, string message);
}
