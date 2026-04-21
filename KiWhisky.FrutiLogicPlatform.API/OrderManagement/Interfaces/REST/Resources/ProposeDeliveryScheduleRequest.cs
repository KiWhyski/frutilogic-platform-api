using System.ComponentModel.DataAnnotations;

namespace KiWhisky.FrutiLogicPlatform.API.OrderManagement.Interfaces.REST.Resources;

public record ProposeDeliveryScheduleRequest(
    [Required] DateTime ProposedDate,
    string? Notes
);

