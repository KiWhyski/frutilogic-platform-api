using Cortex.Mediator;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Infrastructure.Persistence.EFC.Repositories
{
    /// <summary>
    /// This class implements the IAlertRepository interface, providing methods to interact with the Alert aggregate.
    /// </summary>
    public class AlertRepository(AppDbContext context, IMediator mediator) : BaseRepository<Alert>(context, mediator), IAlertRepository
    {
        private readonly IMongoCollection<Alert> _collection = context.GetCollection<Alert>();
        /// <summary>
        /// This async method retrieves all alerts associated with a specific inventory ID.
        /// </summary>
        /// <param name="inventoryId">
        /// The ID of the inventory item whose alerts are to be retrieved.
        /// </param>
        /// <returns>
        /// A list of alerts that belong to the specified inventory ID.
        /// </returns>
        public async Task<List<Alert>> GetAlertsByInventoryId(string inventoryId)
        {
            var targetInventoryId = new InventoryId(inventoryId);
            return await _collection
                .Find(alert => alert.InventoryId == targetInventoryId)
                .ToListAsync();
        }

        /// <summary>
        /// This async method retrieves all alerts associated with a specific account ID.
        /// </summary>
        /// <param name="accountId">
        /// The ID of the account whose alerts are to be retrieved.
        /// </param>
        /// <returns>
        /// The list of alerts that belong to the specified account ID.
        /// </returns>
        public async Task<List<Alert>> GetAllAlertsByAccountId(string accountId)
        {
            var targetAccountId = new AccountId(accountId);
            return await _collection
                .Find(alert => alert.AccountId == targetAccountId)
                .ToListAsync();
        }

        /// <summary>
        /// This method generates a new alert.
        /// </summary>
        /// <param name="accountId">
        /// The ID of the account associated with the alert.
        /// </param>
        /// <param name="type">
        /// The type of the alert.
        /// </param>
        /// <param name="message">
        /// The message of the alert.
        /// </param>
        /// <returns>
        /// The generated alert.
        /// </returns>
        public async Task<Alert> GenerateAlert(string accountId, string type, string message)
        {
            var alert = new Alert(
                title: "System Alert",
                message: message,
                severity: "Medium",
                type: type,
                accountId: new AccountId(accountId),
                inventoryId: new InventoryId() 
            );
            
            await _collection.InsertOneAsync(alert);
            return alert;
        }
    }
}

