using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using Cortex.Mediator;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     Repository implementation for the Warehouse aggregate.
/// </summary>
public class WarehouseRepository(AppDbContext context, IMediator mediator) : BaseRepository<Warehouse>(context, mediator), IWarehouseRepository
{
    
    /// <summary>
    ///     The mongo collection for the Warehouse aggregate.
    /// </summary>
    private readonly IMongoCollection<Warehouse> _warehouseCollection = context.GetCollection<Warehouse>();
    
    /// <summary>
    ///     Method to retrieve all warehouses associated with a specific account ID.
    /// </summary>
    /// <param name="accountId">
    ///     The account ID to filter warehouses by.
    /// </param>
    /// <returns>
    ///     A list of warehouse objects.
    /// </returns>
    public async Task<ICollection<Warehouse>> FindByAccountIdAsync(AccountId accountId)
    {
        return await _warehouseCollection
            .Find(w => w.AccountId == accountId)
            .ToListAsync();
    }

    /// <summary>
    ///     Method to check if a warehouse with the specified name and account ID exists in the database.
    /// </summary>
    /// <param name="name">
    ///     The name of the warehouse to check for existence.
    /// </param>
    /// <param name="accountId">
    ///     The account ID associated with the warehouse.
    /// </param>
    /// <returns>
    ///     A True if a warehouse exists with the specified name and account ID; otherwise, false.
    /// </returns>
    public async Task<bool> ExistByNameIgnoreCaseAndAccountIdAsync(string name, AccountId accountId)
    {
        var filter = Builders<Warehouse>.Filter.And(
            Builders<Warehouse>.Filter.Regex(w => w.Name, new BsonRegularExpression($"^{Regex.Escape(name)}$", "i")),
            Builders<Warehouse>.Filter.Eq(w => w.AccountId, accountId)
        );
        
        return await _warehouseCollection.Find(filter).AnyAsync();
    }

    /// <summary>
    ///     Method to check if a warehouse with the specified name, account ID, and a different warehouse ID exists in the database.
    /// </summary>
    /// <param name="name">
    ///     The name of the warehouse to check for existence.
    /// </param>
    /// <param name="accountId">
    ///     The account ID associated with the warehouse.
    /// </param>
    /// <param name="warehouseId">
    ///     The ID of the warehouse to exclude from the check.
    /// </param>
    /// <returns>
    ///     A True if a warehouse exists with the specified name, account ID, and a different warehouse ID; otherwise, false.
    /// </returns>
    public Task<bool> ExistsByNameIgnoreCaseAndAccountIdAndWarehouseIdIsNotAsync(string name, AccountId accountId, string warehouseId)
    {
        var warehouseObjectId = ObjectId.Parse(warehouseId);
        
        var filter = Builders<Warehouse>.Filter.And(
            Builders<Warehouse>.Filter.Regex(w => w.Name, new BsonRegularExpression($"^{Regex.Escape(name)}$", "i")),
            Builders<Warehouse>.Filter.Eq(w => w.AccountId, accountId),
            Builders<Warehouse>.Filter.Ne(w => w.Id, warehouseObjectId)
        );
        
        return _warehouseCollection.Find(filter).AnyAsync();
    }

    /// <summary>
    ///     Method to check if a warehouse exists by its street, city, postal code, and account ID.
    /// </summary>
    /// <param name="street">
    ///     The street of the warehouse to check for existence.
    /// </param>
    /// <param name="city">
    ///     The city of the warehouse to check for existence.
    /// </param>
    /// <param name="postalCode">
    ///     The postal code of the warehouse to check for existence.
    /// </param>
    /// <param name="accountId">
    ///     The account ID associated with the warehouse.
    /// </param>
    /// <returns>
    ///     A True if a warehouse exists with the exact address components and account ID; otherwise, false.
    /// </returns>
    public Task<bool> ExistsByStreetAndCityAndPostalCodeIgnoreCaseAndAccountIdAsync(string street, string city, string postalCode,
        AccountId accountId)
    {
        var filter = Builders<Warehouse>.Filter.And(
            Builders<Warehouse>.Filter.Regex("Address.Street", new BsonRegularExpression($"^{Regex.Escape(street)}$", "i")),
            Builders<Warehouse>.Filter.Regex("Address.City", new BsonRegularExpression($"^{Regex.Escape(city)}$", "i")),
            Builders<Warehouse>.Filter.Regex("Address.PostalCode", new BsonRegularExpression($"^{Regex.Escape(postalCode)}$", "i")),
            Builders<Warehouse>.Filter.Eq(w => w.AccountId, accountId)
        );
        
        return _warehouseCollection.Find(filter).AnyAsync();
    }

    /// <summary>
    ///     Method to check if a warehouse exists by its address, city, postal code, account ID, and a different warehouse ID.
    /// </summary>
    /// <param name="street">
    ///     The street of the warehouse to check for existence.
    /// </param>
    /// <param name="city">
    ///     The city of the warehouse to check for existence.
    /// </param>
    /// <param name="postalCode">
    ///     The postal code of the warehouse to check for existence.
    /// </param>
    /// <param name="accountId">
    ///     The account ID associated with the warehouse.
    /// </param>
    /// <param name="warehouseId">
    ///     The ID of the warehouse to exclude from the check.
    /// </param>
    /// <returns>
    ///     A True if a warehouse exists with the specified address, city, postal code, account ID, and a different warehouse ID; otherwise, false.
    /// </returns>
    public Task<bool> ExistsByStreetAndCityAndPostalCodeIgnoreCaseAndAccountIdAndWarehouseIdIsNotAsync(string street, string city,
        string postalCode, AccountId accountId, string warehouseId)
    {
        var warehouseObjectId = ObjectId.Parse(warehouseId);
        
        var filter = Builders<Warehouse>.Filter.And(
            Builders<Warehouse>.Filter.Regex("Address.Street", new BsonRegularExpression($"^{Regex.Escape(street)}$", "i")),
            Builders<Warehouse>.Filter.Regex("Address.City", new BsonRegularExpression($"^{Regex.Escape(city)}$", "i")),
            Builders<Warehouse>.Filter.Regex("Address.PostalCode", new BsonRegularExpression($"^{Regex.Escape(postalCode)}$", "i")),
            Builders<Warehouse>.Filter.Eq(w => w.AccountId, accountId),
            Builders<Warehouse>.Filter.Ne(w => w.Id, warehouseObjectId)
        );
        
        return _warehouseCollection.Find(filter).AnyAsync();
    }

    /// <summary>
    ///     Get the account ID associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">
    ///     The unique identifier of the warehouse.
    /// </param>
    /// <returns>
    ///     The account ID associated with the warehouse.
    /// </returns>
    public async Task<string> FindAccountIdByWarehouseIdAsync(string warehouseId)
    {
        var warehouseObjectId = ObjectId.Parse(warehouseId);
        var filter = Builders<Warehouse>.Filter.Eq(w => w.Id, warehouseObjectId);
        var projection = Builders<Warehouse>.Projection.Include(w => w.AccountId).Exclude(w => w.Id);
        var result = await _warehouseCollection.Find(filter).Project<Warehouse>(projection).FirstOrDefaultAsync();
        return result is null ? string.Empty : result.AccountId.GetId;
    }

    /// <summary>
    ///     Get the image URL associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">
    ///     The unique identifier of the warehouse.
    /// </param>
    /// <returns>
    ///     The image URL associated with the warehouse.
    /// </returns>
    public async Task<string> FindImageUrlByWarehouseIdAsync(string warehouseId)
    { 
        var warehouseObjectId = ObjectId.Parse(warehouseId);
        var filter = Builders<Warehouse>.Filter.Eq(w => w.Id, warehouseObjectId);
        var projection = Builders<Warehouse>.Projection.Include(w => w.ImageUrl).Exclude(w => w.Id);
        var result = await _warehouseCollection.Find(filter).Project<Warehouse>(projection).FirstOrDefaultAsync();
        return result is null ? string.Empty : result.ImageUrl.GetValue();
    }

    /// <summary>
    ///     This method counts the number of warehouses associated with a specific account ID.
    /// </summary>
    /// <param name="accountId">
    ///     The unique identifier of the account.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation, containing the count of warehouses.
    /// </returns>
    public Task<int> CountByAccountIdAsync(AccountId accountId)
    {
        var filter = Builders<Warehouse>.Filter.Eq(w => w.AccountId, accountId);
        return _warehouseCollection.CountDocumentsAsync(filter).ContinueWith(t => (int)t.Result);
    }
}
