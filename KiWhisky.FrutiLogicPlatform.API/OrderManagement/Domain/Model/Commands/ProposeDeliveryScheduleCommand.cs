namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Commands;

public record ProposeDeliveryScheduleCommand(
    string OrderId,
    DateTime ProposedDate,
    string? Notes
);

