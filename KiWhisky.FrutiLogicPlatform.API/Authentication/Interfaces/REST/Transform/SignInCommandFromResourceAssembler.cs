using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using System;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Transform
{
    /// <summary>
    ///     Static assembler class to convert SignInResource to SignInCommand. 
    /// </summary>
    public static class SignInCommandFromResourceAssembler
    {
        /// <summary>
        ///     Method to convert SignInResource to SignInCommand.
        /// </summary>
        /// <param name="resource">
        ///     The SignInResource to convert.
        /// </param>
        /// <returns>
        ///     The SignInCommand.
        /// </returns>
        public static SignInCommand ToCommandFromResource(SignInResource resource)
        {
            return new SignInCommand(resource.Email, resource.Password);
        }
    }
}

