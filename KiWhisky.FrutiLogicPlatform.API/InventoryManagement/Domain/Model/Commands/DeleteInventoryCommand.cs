using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Command to delete an inventory by its ID.
/// </summary>
public record DeleteInventoryCommand(ObjectId InventoryId);
