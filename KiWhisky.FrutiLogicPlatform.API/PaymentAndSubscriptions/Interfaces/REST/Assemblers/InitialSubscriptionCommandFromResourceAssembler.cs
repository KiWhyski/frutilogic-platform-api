using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Assemblers;

/// <summary>
///     Static class to convert InitialSubscriptionResource to InitialSubscriptionCommand. 
/// </summary>
public class InitialSubscriptionCommandFromResourceAssembler
{
    /// <summary>
    ///     Method to convert InitialSubscriptionResource to InitialSubscriptionCommand.
    /// </summary>
    /// <param name="resource">
    ///     The selection of plan to use for the subscription.
    /// </param>
    /// <param name="accountId">
    ///     The account id of the account to create the subscription for.
    /// </param>
    /// <returns></returns>
    public static InitialSubscriptionCommand FromCommandToEntity(InitialSubscriptionResource resource, string accountId)
    {
        return new InitialSubscriptionCommand(accountId, resource.SelectedPlanId);
    }
}
