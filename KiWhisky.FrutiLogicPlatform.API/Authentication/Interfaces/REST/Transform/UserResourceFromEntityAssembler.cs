using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Transform
{
    /// <summary>
    ///     Static assembler class to convert User entity to UserResource.   
    /// </summary>
    public static class UserResourceFromEntityAssembler
    {
        /// <summary>
        ///     Method to convert User entity to UserResource. 
        /// </summary>
        /// <param name="entity">
        ///     A User entity.
        /// </param>
        /// <returns>
        ///     A new instance of UserResource.
        /// </returns>
        public static UserResource ToResourceFromEntity(User entity)
        {
            return new UserResource(entity.Id.ToString(), entity.Username, entity.Email.Value);
        }
    }
}

