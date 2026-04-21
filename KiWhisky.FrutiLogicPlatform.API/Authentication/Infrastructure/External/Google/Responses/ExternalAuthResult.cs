namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Infrastructure.External.Google.Responses
{
    /// <summary>
    /// This class is used to store the external authentication result.
    /// It is used to configure the external authentication result in the app settings .json file.
    /// </summary>
    public class ExternalAuthResult
    {
        /// <summary>
        /// The success status of the external authentication.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The provider user ID of the user.
        /// </summary>
        public string? ProviderUserId { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The error message of the external authentication.
        /// </summary>
        public string? Error { get; set; }
    }
}

