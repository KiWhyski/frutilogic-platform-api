namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Queries;

/// <summary>
/// Represents a query to retrieve all alerts for a specific account.
/// </summary>
/// <param name="AccountId">The unique identifier of the account to retrieve alerts for.</param>
public record GetAllAlertsByAccountIdQuery(string accountId);
