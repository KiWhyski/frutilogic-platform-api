using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

/// <summary>
///     Record that represents a query to get all warehouses by account ID.
/// </summary>
public record GetAllWarehousesByAccountId(AccountId AccountId);
