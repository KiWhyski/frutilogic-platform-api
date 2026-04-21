using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Aggregates;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Services
{
    public interface IGoogleAuthService
    {
        Task<(User? user, string? token, string? error)> AuthenticateWithGoogleAsync(string idToken, string? clientId = null);
    }
}

