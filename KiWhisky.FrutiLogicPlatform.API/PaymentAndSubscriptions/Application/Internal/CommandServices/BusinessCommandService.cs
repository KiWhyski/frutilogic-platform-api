using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Application.Internal.CommandServices;

/// <summary>
///     Method to handle the creation of a new business. 
/// </summary>
public class BusinessCommandService(IBusinessRepository businessRepository,
                                    IAccountRepository accountRepository) : IBusinessCommandService
{
    /// <summary>
    ///     Method to handle the creation of a new business.
    /// </summary>
    /// <param name="command">
    ///     The command containing the details for creating a new business.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the newly created business.
    /// </returns>
    public async Task<Business?> Handle(CreateBusinessCommand command)
    {
        var business = new Business(new BusinessName(command.BusinessName));
        await businessRepository.AddAsync(business);
        return business;
    }

    /// <summary>
    ///     Method to handle the update of a business.
    /// </summary>
    /// <param name="command">
    ///     The command containing the details for updating a business.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the updated business.
    /// </returns>
    public async Task<Business?> Handle(UpdateBusinessCommand command)
    {
        var account = await accountRepository.FindByIdAsync(command.AccountId);
        if (account is null) throw new Exception($"Account with ID {command.AccountId} not found.");
        
        var business = await businessRepository.FindByIdAsync(account.BusinessId);
        if (business is null) throw new Exception($"Business with Id {account.BusinessId} not found.");

        business.UpdateBusiness(command);
        await businessRepository.UpdateAsync(business);
        return business;
    }
}
