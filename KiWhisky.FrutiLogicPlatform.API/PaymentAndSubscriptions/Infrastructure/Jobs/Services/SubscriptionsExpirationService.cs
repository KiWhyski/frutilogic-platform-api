using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Application.Internal.OutBoundServices.Jobs.Hosted;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Infrastructure.Jobs.Services;

/// <summary>
///     Service to handle expiration of pending subscriptions.
/// </summary>
public class SubscriptionsExpirationService : ISubscriptionsExpirationService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SubscriptionsExpirationService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    /// <summary>
    ///     Method to check for pending subscriptions that have expired.
    /// </summary>
    public async Task CheckPendingSubscriptionsAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var subscriptionRepository = scope.ServiceProvider.GetRequiredService<ISubscriptionRepository>();

        var pendingSubs = await subscriptionRepository.FindAllPendingUpdateAsync();

        foreach (var sub in pendingSubs)
        {
            if (sub.Status == ESubscriptionStatus.PendingUpgradePayment &&
                sub.UpdatedAt.AddMinutes(15) < DateTime.UtcNow)
            {
                sub.MarkAsCancelledUpdate();
                await subscriptionRepository.UpdateAsync(sub);
            }
        }
    }
}
