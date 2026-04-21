using System.Text.Json.Serialization;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the response returned after successful authentication
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// JWT token for authenticated requests
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// User's unique identifier as a string
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// User's email address
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's username
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Creates a new AuthResponse with the provided values
        /// </summary>
        public static AuthResponse Create(string token, object userId, string email, string username)
        {
            return new AuthResponse
            {
                Token = token,
                UserId = userId?.ToString() ?? string.Empty,
                Email = email,
                Username = username
            };
        }
    }
}

