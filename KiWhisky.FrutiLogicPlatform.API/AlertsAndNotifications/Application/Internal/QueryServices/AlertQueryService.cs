using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Application.Internal.QueryServices
{
    public class AlertQueryService(
        IAlertRepository alertRepository,
        IAlertCommandService alertCommandService,
        IWarehouseRepository warehouseRepository,
        IInventoryRepository inventoryRepository,
        IProductRepository productRepository): IAlertQueryService
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

        public async Task<int> Handle(GenerateExpirationAlertsQuery query)
        {
            if (query.DaysAhead is < 1 or > 30)
                throw new ArgumentOutOfRangeException(nameof(query.DaysAhead), "Days ahead must be between 1 and 30.");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var limit = today.AddDays(query.DaysAhead);
            var generated = 0;
            var warehouses = await warehouseRepository.FindByAccountIdAsync(new AccountId(query.AccountId));

            foreach (var warehouse in warehouses)
            {
                var inventories = await inventoryRepository.FindByWarehouseIdAsync(warehouse.Id);
                foreach (var inventory in inventories)
                {
                    var expirationDate = inventory.ExpirationDate?.GetValue();
                    if (!expirationDate.HasValue || expirationDate.Value < today ||
                        expirationDate.Value > limit || inventory.GetStock() <= 0)
                        continue;

                    var expiration = expirationDate.Value;
                    var idempotencyKey = $"expiration:{inventory.Id}:{expiration:yyyy-MM-dd}";
                    if (await alertRepository.FindByIdempotencyKeyAsync(idempotencyKey) is not null) continue;

                    var product = await productRepository.FindByIdAsync(inventory.ProductId.ToString());
                    if (product is null) continue;

                    var daysUntilExpiration = expiration.DayNumber - today.DayNumber;
                    var details = new AlertDetails(
                        product.Id.ToString(),
                        product.Name,
                        warehouse.Id.ToString(),
                        warehouse.Name,
                        inventory.GetStock(),
                        product.MinimumStock.GetValue(),
                        expiration.ToString("yyyy-MM-dd"),
                        daysUntilExpiration);

                    await alertCommandService.Handle(new CreateAlertCommand(
                        $"Expiration warning: {product.Name}",
                        $"Product {product.Name} in warehouse {warehouse.Name} expires on {expiration:yyyy-MM-dd}.",
                        daysUntilExpiration <= 2 ? "Critical" : "Warning",
                        EAlertTypes.ProductExpired.ToString(),
                        warehouse.AccountId,
                        new InventoryId(inventory.Id.ToString()),
                        details,
                        idempotencyKey));
                    generated++;
                }
            }

            return generated;
        }
    }
}

