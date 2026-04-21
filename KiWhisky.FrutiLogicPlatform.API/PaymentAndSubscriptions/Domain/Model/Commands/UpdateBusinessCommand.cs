namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;

/// <summary>
///     Command for updating a business.
/// </summary>
public record UpdateBusinessCommand(string AccountId,
                                    string BusinessName,
                                    string BusinessEmail,
                                    string Ruc);
