using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Commands
{
    
    /// <summary>
    ///     Command for registering a new user in the system.
    /// </summary>
    public record SignUpCommand(string Name,
                                string Email,
                                string Password,
                                string BusinessName,
                                string Role);
}

