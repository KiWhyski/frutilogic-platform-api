using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Transform
{
    public static class CreateUserCommandFromResourceAssembler
    {
        public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
        {
            return new CreateUserCommand(
                resource.Email, 
                resource.Password, 
                resource.Username, 
                resource.UserRole);
        }
    }
}
