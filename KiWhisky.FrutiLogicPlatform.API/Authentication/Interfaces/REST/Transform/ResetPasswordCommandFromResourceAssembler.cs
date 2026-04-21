using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Transform;

/// <summary>
///     Static assembler class to convert ResetPasswordResource to ResetPasswordCommand.
/// </summary>
public class ResetPasswordCommandFromResourceAssembler
{
    /// <summary>
    ///     Method to convert ResetPasswordResource to ResetPasswordCommand.
    /// </summary>
    /// <param name="resource">
    ///     The ResetPasswordResource to convert.
    /// </param>
    /// <returns>
    ///     A new ResetPasswordCommand.
    /// </returns>
    public static ResetPasswordCommand ToCommandFromResource(ResetPasswordResource resource)
        => new ResetPasswordCommand(resource.Email, resource.NewPassword);
}
