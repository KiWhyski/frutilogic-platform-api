using Cortex.Mediator;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _collection;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        public UserRepository(AppDbContext context, IMediator mediator) : base(context, mediator)
        {
            _collection = context.GetCollection<User>();
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        public async Task<User?> FindByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var normalizedEmail = email.Trim();
            Console.WriteLine($"[UserRepo] FindByEmailAsync: searching '{normalizedEmail}'");

            var objectId = await FindUserObjectIdByEmailAsync(normalizedEmail);
            if (objectId == null)
            {
                Console.WriteLine($"[UserRepo] FindByEmailAsync: objectId not found for '{normalizedEmail}'");
                return null;
            }

            Console.WriteLine($"[UserRepo] FindByEmailAsync: objectId found = {objectId}");
            var user = await _collection.Find(u => u.Id == objectId.Value).FirstOrDefaultAsync();
            if (user == null)
            {
                Console.WriteLine($"[UserRepo] FindByEmailAsync: user not found by objectId {objectId}");
                return null;
            }

            Console.WriteLine($"[UserRepo] FindByEmailAsync: user loaded, Password empty = {string.IsNullOrWhiteSpace(user.Password)}");
            await EnsurePasswordLoadedAsync(user, objectId.Value);
            Console.WriteLine($"[UserRepo] FindByEmailAsync: after EnsurePassword, Password empty = {string.IsNullOrWhiteSpace(user.Password)}");
            return user;
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        public async Task<User?> FindByUsernameAsync(string username)
        {
            return await _collection
                .Find(user => user.Username == username)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a user by their email address or username.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        public async Task<User?> FindByEmailOrUsernameAsync(string email, string username)
        {
            var normalizedEmail = email?.Trim() ?? string.Empty;
            var normalizedUsername = username?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(normalizedEmail))
            {
                var byEmail = await FindByEmailAsync(normalizedEmail);
                if (byEmail != null)
                    return byEmail;
            }

            if (string.IsNullOrWhiteSpace(normalizedUsername))
                return null;

            return await FindByUsernameAsync(normalizedUsername);
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of all user entities.</returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return await _collection
                .Find(_ => true)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Checks if a user with the specified username exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the user exists; otherwise, false.</returns>
        public bool ExistsByUsername(string username)
        {
            return _collection
                .Find(user => user.Username == username)
                .Any();
        }

        /// <summary>
        ///     Method to get all users by account id.
        /// </summary>
        /// <param name="accountId">
        ///     The ID of the account to find users for. 
        /// </param>
        /// <returns>
        ///     A list of users for the specified account.
        /// </returns>
        public async Task<IEnumerable<User?>> GetUsersByAccountIdAsync(string accountId)
        {
            return await _collection
                .Find(user => user.AccountId.ToString() == accountId)
                .ToListAsync();
        }

        /// <summary>
        ///     Method to count users by account id.
        /// </summary>
        /// <param name="accountId">
        ///     The ID of the account to count users for.
        /// </param>
        /// <returns>
        ///     A count of users for the specified account.
        /// </returns>
        public async Task<int> CountByAccountIdAsync(AccountId accountId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.AccountId, accountId);
            return (int)await _collection.CountDocumentsAsync(filter);
        }

        private async Task<ObjectId?> FindUserObjectIdByEmailAsync(string normalizedEmail)
        {
            var collectionName = _collection.CollectionNamespace.CollectionName;
            var bsonCollection = _collection.Database.GetCollection<BsonDocument>(collectionName);
            var escapedEmail = Regex.Escape(normalizedEmail);

            var emailFilter = Builders<BsonDocument>.Filter.Or(
                // Stored as plain string
                Builders<BsonDocument>.Filter.Eq("email", normalizedEmail),
                Builders<BsonDocument>.Filter.Eq("Email", normalizedEmail),
                // Case-insensitive regex on plain string fields
                Builders<BsonDocument>.Filter.Regex("email", new BsonRegularExpression($"^{escapedEmail}$", "i")),
                Builders<BsonDocument>.Filter.Regex("Email", new BsonRegularExpression($"^{escapedEmail}$", "i")),
                // Stored as nested value object: { "email": { "value": "..." } }
                Builders<BsonDocument>.Filter.Eq("email.value", normalizedEmail),
                Builders<BsonDocument>.Filter.Eq("email.Value", normalizedEmail),
                // Stored as nested value object: { "Email": { "Value": "..." } } (PascalCase C# serialization)
                Builders<BsonDocument>.Filter.Eq("Email.value", normalizedEmail),
                Builders<BsonDocument>.Filter.Eq("Email.Value", normalizedEmail));

            var document = await bsonCollection.Find(emailFilter).FirstOrDefaultAsync();
            if (document == null || !document.Contains("_id"))
                return null;

            return document["_id"].AsObjectId;
        }

        private async Task EnsurePasswordLoadedAsync(User user, ObjectId objectId)
        {
            if (!string.IsNullOrWhiteSpace(user.Password))
                return;

            var collectionName = _collection.CollectionNamespace.CollectionName;
            var bsonCollection = _collection.Database.GetCollection<BsonDocument>(collectionName);
            var document = await bsonCollection
                .Find(Builders<BsonDocument>.Filter.Eq("_id", objectId))
                .Project(Builders<BsonDocument>.Projection.Include("password").Include("Password"))
                .FirstOrDefaultAsync();

            if (document == null)
                return;

            if (document.TryGetValue("password", out var passwordValue) && passwordValue.IsString)
                user.Password = passwordValue.AsString;
            else if (document.TryGetValue("Password", out var legacyPassword) && legacyPassword.IsString)
                user.Password = legacyPassword.AsString;
        }
    }
}
