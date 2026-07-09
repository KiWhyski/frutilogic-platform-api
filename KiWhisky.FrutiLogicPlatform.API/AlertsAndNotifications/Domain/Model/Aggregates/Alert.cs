using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;

public class Alert: Entity
{
    /// <summary>
    /// The title of the alert, providing a brief description of the alert.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// The message of the alert, providing detailed information about the alert.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// The severity of the alert, indicating its importance or urgency.
    /// </summary>
    public ESeverityTypes Severity { get; private set; }

    /// <summary>
    /// The type of the alert, categorizing it into a specific type.
    /// </summary>
    public EAlertTypes Type { get; private set; }

    /// <summary>
    /// The date and time when the alert was created, initialized to the current UTC time.
    /// </summary>
    public DateTime GeneratedAt { get; private set; }

    /// <summary>
    /// The unique identifier of the profile associated with the alert.
    /// </summary>
    public AccountId AccountId { get; private set; }

    /// <summary>
    /// The unique identifier of the inventory item associated with the alert.
    /// <summay>
    public InventoryId InventoryId { get; private set; }

    public AlertDetails? Details { get; private set; }

    public string? IdempotencyKey { get; private set; }

    /// <summary>
    /// Parameterless constructor required by Entity Framework for materialization.
    /// </summary>
    public Alert() { }

    /// <summary>
    /// Constructor for creating a new alert.
    /// </summary>
    /// <param name="title">The title of the alert.</param>
    /// <param name="message">The message of the alert.</param>
    /// <param name="severity">The severity of the alert.</param>
    /// <param name="type">The type of the alert.</param>
    /// <param name="accountId">The ID of the profile associated with the alert.</param>
    /// <param name="inventoryId">The ID of the inventory associated with the alert.</param>
    public Alert(string title, string message, string severity, string type, AccountId accountId,
        InventoryId inventoryId, AlertDetails? details = null, string? idempotencyKey = null)
    {
        Id = ObjectId.GenerateNewId();
        Title = title;
        Message = message;
        Severity = Enum.Parse<ESeverityTypes>(severity, true);
        Type = Enum.Parse<EAlertTypes>(type, true);
        GeneratedAt = DateTime.UtcNow;
        AccountId = accountId;
        InventoryId = inventoryId;
        Details = details;
        IdempotencyKey = idempotencyKey;
    }

    /// <summary>
    /// Constructor for creating a new alert.
    /// </summary>
    /// <param name="command">The command containing the alert details.</param>
    public Alert(CreateAlertCommand command):this(command.Title, command.Message, command.Severity, command.Type,
        command.AccountId, command.InventoryId, command.Details, command.IdempotencyKey) { }

}
