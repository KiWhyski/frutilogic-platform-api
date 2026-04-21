using System.ComponentModel.DataAnnotations;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Interfaces.REST.Resources;

public record RespondDeliveryProposalRequest(
    [Required] bool Accept,
    string? Notes
);

