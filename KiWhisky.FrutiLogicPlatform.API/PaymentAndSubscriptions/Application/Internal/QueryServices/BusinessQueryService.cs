using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Application.Internal.QueryServices;

/// <summary>
///     Service implementation for handling business-related queries.
/// </summary>
public class BusinessQueryService(IAccountRepository accountRepository, 
                                  IBusinessRepository businessRepository) : IBusinessQueryService
{
    /// <summary>
    ///     Method to handle the retrieval of a business by its associated account ID.
    /// </summary>
    /// <param name="query">
    ///     The query object containing the account ID.
    /// </param>
    /// <returns>
    ///     The business associated with the account ID.   
    /// </returns>
    public async Task<Business> Handle(GetBusinessByAccountIdQuery query)
    {
        var account = await accountRepository.FindByIdAsync(query.AccountId);
        if (account is null) throw new Exception($"Account with ID {query.AccountId} not found.");
        
        var business = await businessRepository.FindByIdAsync(account.BusinessId);
        if (business is null) throw new Exception($"Business with Id {account.BusinessId} not found.");
        
        return business;
    }
}
