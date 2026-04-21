using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Commands;

public record CreateUserCommand(Email Email, string Password, string Username,string UserRole);
