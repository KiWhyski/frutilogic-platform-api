using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Interfaces.REST.Assemblers;

public class UpdateProfileCommandFromResourceAssembler
{
    /// <summary>
    /// Converts an UpdateProfileResource to an UpdateProfileInformationCommand.
    /// </summary>
    /// <param name="profileId">The ID of the profile to update.</param>
    /// <param name="resource">The update profile resource.</param>
    /// <returns>The update profile information command.</returns>
    public static UpdateProfileCommand ToCommandFromResource(string profileId, UpdateProfileResource resource)
    {
        var personName = new PersonName(resource.FirstName, resource.LastName);
        var personContactNumber = new PersonContactNumber(resource.PhoneNumber);

        return new UpdateProfileCommand(
            profileId,
            personName,
            personContactNumber,
            resource.PhoneNumber,
            resource.ProfilePicture,
            resource.AssignedRole
        );
    }
}
