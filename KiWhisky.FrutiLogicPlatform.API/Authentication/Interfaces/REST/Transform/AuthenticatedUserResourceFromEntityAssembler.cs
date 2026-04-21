using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Transform;

/// <summary>
///     Static assembler class to convert User entity to AuthenticatedUserResource.
/// </summary>
public static class AuthenticatedUserResourceFromEntityAssembler
{
    /// <summary>
    ///     Method to convert User entity to AuthenticatedUserResource.  
    /// </summary>
    /// <param name="user">
    ///     The User entity to convert. 
    /// </param>
    /// <param name="token">
    ///     The JWT token to include in the response.
    /// </param>
    /// <returns>
    ///     An AuthenticatedUserResource containing the converted User entity.
    /// </returns>
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        return new AuthenticatedUserResource(token, user.Id.ToString(), user.Email.Value, user.Username, user.AccountId.GetId);
    }
}
