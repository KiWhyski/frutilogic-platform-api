namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Application.Internal.Dtos;

public record UsersWithStatsDto(
    int? MaxUsersAllowed,
    int TotalUsers,
    IEnumerable<UsersWithProfilesDto> Users
);
