namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;

/// <summary>
///     Command to create a new business.
/// </summary>
/// <param name="BusinessName">
///     The name of the business to be created.
/// </param>
public record CreateBusinessCommand(string BusinessName);
