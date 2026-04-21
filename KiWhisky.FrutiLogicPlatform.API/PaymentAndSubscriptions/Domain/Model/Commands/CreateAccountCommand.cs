namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;



/// <summary>
///     The command to create a new account.
/// </summary>
/// <param name="BusinessId">
///     The ID of the business associated with the account.
/// </param>
/// <param name="AccountRole">
///     The role of the account.
/// </param>
public record CreateAccountCommand(
        string BusinessId,
        string AccountRole
    );
