namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;

/// <summary>
///     Query to get a business by its associated account ID.
/// </summary>
/// <param name="AccountId">
///     The unique identifier of the account whose business is to be retrieved.
/// </param>
public record GetBusinessByAccountIdQuery(string AccountId);
