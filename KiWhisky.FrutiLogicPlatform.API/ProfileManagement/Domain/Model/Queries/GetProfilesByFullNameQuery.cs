namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.Queries;

/// <summary>
/// Query to get profiles by full name.
/// </summary>
/// <param name="FullName">The full name to search for.</param>
public record GetProfilesByFullNameQuery(string FullName);
