namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;

/// <summary>
///     Query to get subscription details by account ID.
/// </summary>
/// <param name="AccountId">
///     The unique identifier of the account whose subscription details are to be retrieved.
/// </param>
public record GetSubscriptionDetailsByAccountIdQuery(string AccountId);
