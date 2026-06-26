namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Configuration;

public static class MongoConnection
{
    public static string? ResolveConnectionString(IConfiguration configuration)
    {
        var candidates = new[]
        {
            configuration["MongoDB:ConnectionString"],
            configuration["MONGO_URL"],
            configuration["DATABASE_URL"],
            Environment.GetEnvironmentVariable("MongoDB__ConnectionString"),
            Environment.GetEnvironmentVariable("MONGO_URL"),
            Environment.GetEnvironmentVariable("DATABASE_URL"),
        };

        foreach (var candidate in candidates)
        {
            if (!string.IsNullOrWhiteSpace(candidate))
            {
                return Normalize(candidate);
            }
        }

        var host = Environment.GetEnvironmentVariable("MONGOHOST")
                   ?? Environment.GetEnvironmentVariable("MONGODB_HOST");
        var port = Environment.GetEnvironmentVariable("MONGOPORT")
                   ?? Environment.GetEnvironmentVariable("MONGODB_PORT")
                   ?? "27017";
        var user = Environment.GetEnvironmentVariable("MONGOUSER")
                   ?? Environment.GetEnvironmentVariable("MONGODB_USER");
        var password = Environment.GetEnvironmentVariable("MONGOPASSWORD")
                       ?? Environment.GetEnvironmentVariable("MONGODB_PASSWORD");

        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        return Normalize(
            $"mongodb://{user}:{Uri.EscapeDataString(password)}@{host}:{port}/?authSource=admin");
    }

    public static string Normalize(string connectionString)
    {
        if (connectionString.Contains("authSource=", StringComparison.OrdinalIgnoreCase))
        {
            return connectionString;
        }

        return connectionString.Contains('?')
            ? $"{connectionString}&authSource=admin"
            : $"{connectionString}/?authSource=admin";
    }

    public static string MaskHost(string connectionString)
    {
        try
        {
            return new Uri(connectionString).Host;
        }
        catch
        {
            return "unknown";
        }
    }
}
