using MongoDB.Bson;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
///     Command to represent the action of transferring products from one warehouse to another.
/// </summary>
public record TransferProductsToAnotherWarehouseCommand(ObjectId ProductId, ObjectId OriginWarehouseId, DateTime? ExpirationDate, int QuantityToTransfer, ObjectId DestinationWarehouseId);
