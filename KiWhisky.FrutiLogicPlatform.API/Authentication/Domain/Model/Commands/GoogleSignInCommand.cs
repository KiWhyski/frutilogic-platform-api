using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Aggregates;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Commands
{
    public record GoogleSignInCommand(
        string IdToken,
        string? ClientId = null
    );
}

