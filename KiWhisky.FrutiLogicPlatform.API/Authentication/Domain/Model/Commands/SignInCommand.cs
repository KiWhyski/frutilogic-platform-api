using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Commands
{
    /**
    * <summary>
    *     The sign in command
    * </summary>
    * <remarks>
    *     This command object includes the username and password to sign in
    * </remarks>
    */
    public record SignInCommand(string Email, string Password);
}

