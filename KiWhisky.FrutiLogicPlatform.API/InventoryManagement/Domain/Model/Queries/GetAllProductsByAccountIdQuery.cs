using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
///     Query to get all products for a given account.
/// </summary>
public record GetAllProductsByAccountIdQuery(AccountId AccountId);
