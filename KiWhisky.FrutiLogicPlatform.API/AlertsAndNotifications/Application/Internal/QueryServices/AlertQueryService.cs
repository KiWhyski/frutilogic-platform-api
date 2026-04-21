using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Services;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Application.Internal.QueryServices
{
    public class AlertQueryService(IAlertRepository alertRepository): IAlertQueryService
    {
        /// <summary>
        /// This method retrieves an alert by its ID.
        /// </summary>
        /// <param name="query">
        /// The query containing the alert ID.
        /// </param>
        /// <returns>
        /// The alert with the specified ID, or null if not found.
        /// </returns>
        public async Task<Alert?> Handle(GetAlertByIdQuery query)
        {
            return await alertRepository.FindByIdAsync(query.AlertId);
        }
        /// <summary>
        /// This async method retrieves all alerts for a specific inventory ID.
        /// </summary>
        /// <param name="query">
        /// The query containing the inventory ID.
        /// </param>
        /// <returns>
        /// A list of alerts associated with the specified inventory ID.
        /// </returns>
        public async Task<IEnumerable<Alert>> Handle(GetAllAlertsByInventoryIdQuery query) 
        {
            return await alertRepository.GetAlertsByInventoryId(query.InventoryId.GetId);
        }
        /// <summary>
        /// This async method retrieves all alerts for a specific profile ID.
        /// </summary>
        /// <param name="query">
        /// The query containing the profile ID.
        /// </param>
        /// <returns>
        /// A list of alerts associated with the specified profile ID.
        /// </returns>
        public async Task<IEnumerable<Alert>> Handle(GetAllAlertsByAccountIdQuery query)
        {
            return await alertRepository.GetAllAlertsByAccountId(query.accountId);
        }
    }
}

