namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Commands;

public record GenerateAlertCommand(string AccountId,string Title, string Type, string Message);
