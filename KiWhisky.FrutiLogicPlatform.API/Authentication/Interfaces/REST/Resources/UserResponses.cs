namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents a user in the user list response
    /// </summary>
    public class UserListResponse
    {
        /// <summary>
        /// The unique identifier for the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user's email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if the user's email has been verified
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// The authentication provider (e.g., "Google")
        /// </summary>
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// When the user account was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When the user last logged in
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Indicates if the user account is disabled
        /// </summary>
        public bool IsDisabled { get; set; }
    }

    /// <summary>
    /// Represents a paginated response
    /// </summary>
    /// <typeparam name="T">The type of items in the response</typeparam>
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// The items in the current page
        /// </summary>
        public List<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// The current page number (1-based)
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The number of items per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of items across all pages
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Indicates if there is a next page
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Indicates if there is a previous page
        /// </summary>
        public bool HasPreviousPage { get; set; }
    }
}

