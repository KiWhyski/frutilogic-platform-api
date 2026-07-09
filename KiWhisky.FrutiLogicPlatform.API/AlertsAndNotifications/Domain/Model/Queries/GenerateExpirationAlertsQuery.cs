namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Queries;

public record GenerateExpirationAlertsQuery(string AccountId, int DaysAhead);
