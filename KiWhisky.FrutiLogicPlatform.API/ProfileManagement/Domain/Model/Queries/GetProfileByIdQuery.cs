namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.Queries;

/// <summary>
/// Query to get a profile by its ID.
/// </summary>
/// <param name="ProfileId">The ID of the profile to retrieve.</param>
public record GetProfileByIdQuery(string ProfileId);
