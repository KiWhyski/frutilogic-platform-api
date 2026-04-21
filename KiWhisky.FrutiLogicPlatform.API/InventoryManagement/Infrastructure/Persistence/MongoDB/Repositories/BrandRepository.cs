using Cortex.Mediator;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     Repository implementation for the Brand entity.
/// </summary>
public class BrandRepository(AppDbContext context, IMediator mediator) : BaseRepository<Brand>(context, mediator), IBrandRepository
{
    /// <summary>
    ///     The MongoDB collection for the Brand entity.   
    /// </summary>
    private readonly IMongoCollection<Brand> _brandCollection = context.GetCollection<Brand>();
    
    /// <summary>
    ///     Method to seed the brand names in the database.
    /// </summary>
    /// <returns>
    ///     A confirmation of the seeding operation.
    /// </returns>
    public async Task SeedBrandNamesAsync()
    {
        // Get the list of brand names from the enum
        var nameList = Enum.GetValues<EBrandNames>()
            .Select(m => m.ToString())
            .ToList();

        // Verifies which names already exist in the database
        var existingNames = await _brandCollection
            .Find(FilterDefinition<Brand>.Empty)
            .Project(m => m.Name)
            .ToListAsync();
        
        // Determine which names need to be added
        var namesToAdd = nameList
            .Where(name => !existingNames.Contains(Enum.Parse<EBrandNames>(name)))
            .Select(name => new Brand(Enum.Parse<EBrandNames>(name)))
            .ToList();

        // Insert the new names into the database
        if (namesToAdd.Count != 0) await _brandCollection.InsertManyAsync(namesToAdd);
    }
}
