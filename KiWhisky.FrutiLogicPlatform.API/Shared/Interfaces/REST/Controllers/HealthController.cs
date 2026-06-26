using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/health")]
public class HealthController(IMongoClient mongoClient, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            await mongoClient.GetDatabase("admin")
                .RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1), cancellationToken: cancellationToken);

            return Ok(new
            {
                status = "healthy",
                database = configuration["MongoDB:DatabaseName"] ?? "stocksip_platform",
            });
        }
        catch (Exception ex)
        {
            return StatusCode(503, new
            {
                status = "unhealthy",
                error = ex.Message,
            });
        }
    }
}
