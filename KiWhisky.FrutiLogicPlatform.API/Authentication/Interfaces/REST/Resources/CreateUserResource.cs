using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources
{
    /// <summary>
    ///     Resource for the sign-up request
    /// </summary>
    public record CreateUserResource(Email Email, string Password, string Username, string UserRole);
}

