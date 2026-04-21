using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Application.Internal.QueryServices;

/// <summary>
///     Implementation of the <see cref="IAccountQueryService"/> interface.
/// </summary>
/// <param name="accountRepository">
///     The repository for handling account-related operations.
/// </param>
public class AccountQueryService(IAccountRepository accountRepository) : IAccountQueryService
{
    /// <summary>
    ///     Method to handle the retrieval of an account by its ID.   
    /// </summary>
    /// <param name="query">
    ///     The query object containing the account ID. 
    /// </param>
    /// <returns>
    ///     The account with the specified ID.
    /// </returns>
    public async Task<Account?> Handle(GetAccountByIdQuery query)
    {
        return await accountRepository.FindByIdAsync(query.AccountId);
    }

    public async Task<string?> Handle(GetAccountStatusByIdQuery query)
    {
        return await accountRepository.GetAccountStatusByIdAsync(query.AccountId);
    }
    
    public async Task<IEnumerable<Account>> Handle(GetAccountsByRoleQuery query)
    {
        return await accountRepository.FindAccountsByRoleAsync(query.Role);
    }
}
