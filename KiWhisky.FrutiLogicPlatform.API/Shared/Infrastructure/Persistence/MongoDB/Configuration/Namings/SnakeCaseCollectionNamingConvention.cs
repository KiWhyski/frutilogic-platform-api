using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Extensions;
using MongoDB.Driver;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Namings;

public static class SnakeCaseCollectionNamingConvention
{
    /// <summary>
    ///     Use camel case naming convention
    /// </summary>
    /// <remarks>
    ///     This method sets the naming convention for the database collections to camel case.
    /// </remarks>
    public static IMongoCollection<T> GetCollectionWithConvention<T>(this IMongoDatabase database)
    {
        // Base name of the type T
        var baseName = typeof(T).Name;

        // Applies pluralization and camel case conversion to the collection name
        var collectionName = baseName.ToPlural().ToSnakeCase();

        // Returns the collection with the modified name
        return database.GetCollection<T>(collectionName);
    }
}
