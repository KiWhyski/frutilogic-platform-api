using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Interfaces.REST.Resources;

/// <summary>
///     Record representing the authenticated user resource.
/// </summary>
public record AuthenticatedUserResource(
        string Token,
        string UserId,
        string Email,
        string Username,
        string AccountId
    );
