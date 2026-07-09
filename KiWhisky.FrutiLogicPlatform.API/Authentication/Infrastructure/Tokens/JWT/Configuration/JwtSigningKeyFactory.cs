using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Infrastructure.Tokens.JWT.Configuration;

/// <summary>
/// Builds a symmetric signing key that always meets HMAC-SHA256 minimum size (256 bits).
/// </summary>
public static class JwtSigningKeyFactory
{
    private const int MinimumKeyBytes = 32;

    public static SymmetricSecurityKey CreateSecurityKey(string secret)
    {
        if (string.IsNullOrWhiteSpace(secret))
            throw new InvalidOperationException("JWT Secret is not configured");

        return new SymmetricSecurityKey(CreateKeyBytes(secret.Trim()));
    }

    public static byte[] CreateKeyBytes(string secret)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secret);
        return keyBytes.Length >= MinimumKeyBytes
            ? keyBytes
            : SHA256.HashData(keyBytes);
    }
}
