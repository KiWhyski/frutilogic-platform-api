using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.ACL.Services;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Application.External.ACL;


/// <summary>
///     Implementation of the <see cref="IPaymentAndSubscriptionsFacade"/> interface.
/// </summary>
public class PaymentAndSubscriptionsFacade(
    IAccountCommandService accountCommandService,
    IBusinessCommandService businessCommandService,
    ISubscriptionQueryService subscriptionQueryService,
    IAccountQueryService accountQueryService,
    IBusinessQueryService businessQueryService,
    ISubscriptionsCommandService subscriptionsCommandService,
    IPlanQueryService planQueryService
) : IPaymentAndSubscriptionsFacade
{
    /// <summary>
    ///     Creates a new account.
    /// </summary>
    public async Task<Account?> CreateAccount(string role, string businessId)
    {
        var command = new CreateAccountCommand(businessId, role);
        var account = await accountCommandService.Handle(command);
        return account;
    }

    /// <summary>
    ///     Creates a new business.
    /// </summary>
    public async Task<Business?> CreateBusiness(string businessName)
    {
        var command = new CreateBusinessCommand(businessName);
        var business = await businessCommandService.Handle(command);
        return business;
    }

    /// <summary>
    ///     Gets the warehouse limit of the plan associated with the account.
    /// </summary>
    public async Task<int?> GetPlanWarehouseLimitByAccountId(string accountId)
    {
        var query = new GetPlanWarehouseLimitByAccountId(accountId);
        return await subscriptionQueryService.Handle(query);
    }

    /// <summary>
    ///     Gets the products limit of the plan associated with the account.
    /// </summary>
    public async Task<int?> GetPlanProductsLimitByAccountId(string accountId)
    {
        var query = new GetPlanProductsLimitByAccountIdQuery(accountId);
        return await subscriptionQueryService.Handle(query);
    }

    /// <summary>
    ///     Gets the user limit of the plan associated with the account.
    /// </summary>
    public async Task<int?> GetPlanUserLimitByAccountId(string accountId)
    {
        var query = new GetPlanUsersLimitByAccountIdQuery(accountId);
        return await subscriptionQueryService.Handle(query);
    }

    /// <summary>
    ///     Gets all addresses associated with an account.
    /// </summary>
    public async Task<IEnumerable<Address>> GetAccountAddressesAsync(string accountId)
    {
        var query = new GetAccountByIdQuery(accountId);
        var account = await accountQueryService.Handle(query);
        
        return account?.Addresses ?? Enumerable.Empty<Address>();
    }

    /// <summary>
    ///     Gets a specific address by account and address index.
    /// </summary>
    public async Task<Address?> GetAccountAddressAsync(string accountId, int addressIndex)
    {
        var addresses = await GetAccountAddressesAsync(accountId);
        var addressList = addresses.ToList();

        return (addressIndex >= 0 && addressIndex < addressList.Count)
            ? addressList[addressIndex]
            : null;
    }

    /// <summary>
    ///     Finds an account by its identifier.
    /// </summary>
    public async Task<Account?> FindAccountByIdAsync(string accountId)
    {
        var query = new GetAccountByIdQuery(accountId);
        return await accountQueryService.Handle(query);
    }

    /// <summary>
    ///     Finds the business associated with a given account.
    /// </summary>
    public async Task<Business?> FindBusinessByAccountIdAsync(string accountId)
    {
        var query = new GetBusinessByAccountIdQuery(accountId);
        return await businessQueryService.Handle(query);
    }

    public async Task ActivateFreePlanForAccountAsync(string accountId)
    {
        var plans = await planQueryService.Handle(new GetAllPlansQuery());
        var freePlan = plans.FirstOrDefault(p => p.PlanType == EPlanType.Free);
        if (freePlan is null)
            return;

        try
        {
            await subscriptionsCommandService.Handle(
                new InitialSubscriptionCommand(accountId, freePlan.Id.ToString()));
        }
        catch (Exception ex) when (ex.Message.Contains("active subscription already exists", StringComparison.OrdinalIgnoreCase))
        {
            // Account already has a subscription — nothing to do.
        }
    }
}
