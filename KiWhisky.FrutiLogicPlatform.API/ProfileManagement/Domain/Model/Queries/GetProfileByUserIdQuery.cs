namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.Queries;

/// <summary>
/// Query to get a profile by user ID.
/// </summary>
/// <param name="UserId">The user ID associated with the profile.</param>
public record GetProfileByUserIdQuery(string UserId);
