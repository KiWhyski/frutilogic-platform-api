namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Model.Commands;

public record RespondDeliveryProposalCommand(
    string OrderId,
    bool Accept,
    string? Notes
);

