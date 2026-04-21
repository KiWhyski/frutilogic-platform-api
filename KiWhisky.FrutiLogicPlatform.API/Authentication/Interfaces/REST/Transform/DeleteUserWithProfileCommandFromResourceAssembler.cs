using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Commands;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Transform;

/// <summary>
///     Class to convert DeleteUserWithProfileResource to DeleteUserWithProfileCommand.
/// </summary>
public class DeleteUserWithProfileCommandFromResourceAssembler
{
    /// <summary>
    ///     Method to convert DeleteUserWithProfileResource to DeleteUserWithProfileCommand.
    /// </summary>
    /// <param name="userId">
    ///     The ID of the user to delete.
    /// </param>
    /// <param name="profileId">
    ///     The ID of the profile to delete.
    /// </param>
    /// <returns>
    ///     A DeleteUserWithProfileCommand representing the user and profile to delete.
    /// </returns>
    public static DeleteUserWithProfielByIdCommand ToCommandFromResource(string userId, string profileId)
    {
        return new DeleteUserWithProfielByIdCommand(userId, profileId);
    }
}
