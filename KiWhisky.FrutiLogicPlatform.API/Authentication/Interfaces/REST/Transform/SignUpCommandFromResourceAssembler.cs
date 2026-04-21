using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Transform
{
    /// <summary>
    ///     Static assembler class to convert SignUpResource to SignUpCommand.  
    /// </summary>
    public static class SignUpCommandFromResourceAssembler
    {
        /// <summary>
        ///     Method to convert SignUpResource to SignUpCommand. 
        /// </summary>
        /// <param name="resource">
        ///     The SignUpResource to convert.
        /// </param>
        /// <returns>
        ///     A new SignUpCommand.
        /// </returns>
        public static SignUpCommand ToCommandFromResource(SignUpResource resource)
        {
            return new SignUpCommand(
                resource.Name,
                resource.Email, 
                resource.Password,
                resource.BusinessName,
                resource.Role
                );
        }
    }
}

