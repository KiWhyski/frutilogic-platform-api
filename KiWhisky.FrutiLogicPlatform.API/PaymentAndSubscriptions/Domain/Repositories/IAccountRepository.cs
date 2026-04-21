using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Repositories;

/// <summary>
///     Repository interface for managing Account entities.
/// </summary>
public interface IAccountRepository : IBaseRepository<Account>
{
    /// <summary>
    ///     Method to get the status of an account by its ID.
    /// </summary>
    /// <param name="accountId">
    ///     The ID of the account whose status is to be retrieved.
    /// </param>
    /// <returns>
    ///     The status of the account.
    /// </returns>
    Task<string?> GetAccountStatusByIdAsync(string accountId);

    /// <summary>
    /// Method to find accounts based on their assigned role.
    /// </summary>
    /// <param name="role">
    /// The role of the accounts to be retrieved.
    /// Can be null to include accounts with all roles.
    /// </param>
    /// <returns>
    /// A collection of accounts that have the specified role.
    /// </returns>
    Task<IEnumerable<Account>> FindAccountsByRoleAsync(string? role);
}
