namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;

/// <summary>
///     Query to get an account by its ID.
/// </summary>
/// <param name="AccountId">
///     The unique identifier of the account to retrieve.
/// </param>
public record GetAccountByIdQuery(string AccountId);
