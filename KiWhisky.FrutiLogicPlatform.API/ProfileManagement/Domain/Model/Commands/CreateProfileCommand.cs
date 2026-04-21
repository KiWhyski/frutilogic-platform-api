using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.Commands;

/// <summary>
/// Command to register a new profile.
/// </summary>
/// <param name="Name">The name of the person.</param>
/// <param name="PersonContactNumber">The contact number of the person.</param>
/// <param name="ContactNumber">The contact number as string.</param>
/// <param name="ProfilePicture">The profile picture.</param>
/// <param name="UserId">The user ID associated with this profile.</param>
public record CreateProfileCommand(
    PersonName Name,
    PersonContactNumber PersonContactNumber,
    string ContactNumber,
    IFormFile? ProfilePicture,
    string UserId,
    string AssignedRole
);
