using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Aggregates;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Application.Internal.OutboundServices.Token
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates a token for the given user.
        /// </summary>
        string GenerateToken(User Username);

        /// <summary>
        /// Validates the provided token and returns the user ID if valid.
        /// </summary>
        Task<string?> ValidateToken(string token);
    }
}
