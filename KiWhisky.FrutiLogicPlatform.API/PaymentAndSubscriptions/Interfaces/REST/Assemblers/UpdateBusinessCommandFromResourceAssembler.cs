using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Assemblers;

/// <summary>
///     Static class to convert UpdateBusinessResource to UpdateBusinessCommand.
/// </summary>
public class UpdateBusinessCommandFromResourceAssembler
{
    /// <summary>
    ///     Method to convert UpdateBusinessResource to UpdateBusinessCommand.
    /// </summary>
    /// <param name="resource">
    ///     The UpdateBusinessResource to convert.
    /// </param>
    /// <param name="accountId">
    ///     The account id of the business to update.
    /// </param>
    /// <returns>
    ///     A new instance of UpdateBusinessCommand.
    /// </returns>
    public static UpdateBusinessCommand FromCommandToEntity(UpdateBusinessResource resource, string accountId)
    {
        return new UpdateBusinessCommand(
            accountId,
            resource.BusinessName,
            resource.BusinessEmail,
            resource.Ruc
        );
    }
}
