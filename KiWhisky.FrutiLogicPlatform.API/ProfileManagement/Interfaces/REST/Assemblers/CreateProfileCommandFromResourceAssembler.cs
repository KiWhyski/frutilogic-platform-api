using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Interfaces.REST.Assemblers;

public class CreateProfileCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a CreateProfileResource to a RegisterProfileCommand.
    /// </summary>
    /// <param name="resource">The create profile resource.</param>
    /// <returns>The register profile command.</returns>
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        var personName = new PersonName(resource.FirstName, resource.LastName);
        var personContactNumber = new PersonContactNumber(resource.PhoneNumber);

        return new CreateProfileCommand(
            personName,
            personContactNumber,
            resource.PhoneNumber,
            resource.ProfilePicture,
            resource.UserId,
            resource.AssignedRole
        );
    }
}
