using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Application.Internal.CommandServices
{
    /// <summary>
    /// This class implements the command service for handling alert-related commands.
    /// </summary>
    /// <param name="alertRepository">
    /// The repository for managing alerts.
    /// </param>
    /// <param name="unitOfWork">
    /// The unit of work for managing transactions.
    /// </param>
    public class AlertCommandService(IAlertRepository alertRepository,
    IUnitOfWork unitOfWork): IAlertCommandService
    {
        /// <summary>
        /// Handles the creation of a new alert.
        /// </summary>
        /// <param name="command">The command containing the alert details.</param>
        /// <returns>The created alert.</returns>
        public async Task<Alert?> Handle(CreateAlertCommand command)
        {
            if (!string.IsNullOrWhiteSpace(command.IdempotencyKey))
            {
                var existingAlert = await alertRepository.FindByIdempotencyKeyAsync(command.IdempotencyKey);
                if (existingAlert is not null) return existingAlert;
            }

            var alert = new Alert(command);
            await alertRepository.AddAsync(alert);
            await unitOfWork.CompleteAsync();
            return alert;
        }
    }
}

