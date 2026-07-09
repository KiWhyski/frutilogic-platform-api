using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using MongoDB.Bson;
using MongoDB.Driver;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/care-guides")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Available Care Guide Endpoints")]
    public class CareGuidesController(
        ICareGuideCommandService careGuideCommandService,
        ICareGuideQueryService careGuideQueryService,
        IProductQueryService productQueryService,
        AppDbContext dbContext)
        : ControllerBase
    {
        private readonly IMongoCollection<Product> _products = dbContext.GetCollection<Product>();

        private async Task<(string ProductId, string ImageUrl)?> TryResolveProductByNameAsync(string accountId, string productName)
        {
            if (string.IsNullOrWhiteSpace(accountId) || string.IsNullOrWhiteSpace(productName)) return null;
            var filter = Builders<Product>.Filter.And(
                Builders<Product>.Filter.Eq(p => p.AccountId, new AccountId(accountId)),
                Builders<Product>.Filter.Eq(p => p.Name, productName)
            );
            var prod = await _products.Find(filter).FirstOrDefaultAsync();
            if (prod == null) return null;
            return (prod.Id.ToString(), prod.ImageUrl.GetValue());
        }

        private async Task<CareGuideResource> EnrichWithProductInfo(CareGuideResource resource)
        {
            if (string.IsNullOrWhiteSpace(resource.ProductId)) return resource;
            try
            {
                var productObjectId = ObjectId.Parse(resource.ProductId);
                var product = await productQueryService.Handle(new GetProductByIdQuery(productObjectId));
                if (product == null) return resource;

                return new CareGuideResource(
                    CareGuideId: resource.CareGuideId,
                    AccountId: resource.AccountId,
                    ProductId: resource.ProductId,
                    Title: resource.Title,
                    Summary: resource.Summary,
                    RecommendedMinTemperature: resource.RecommendedMinTemperature,
                    RecommendedMaxTemperature: resource.RecommendedMaxTemperature,
                    RecommendedPlaceStorage: resource.RecommendedPlaceStorage,
                    GeneralRecommendation: resource.GeneralRecommendation,
                    TypeOfLiquor: product.Type.ToString(),
                    ProductName: product.Name,
                    ImageUrl: product.ImageUrl.GetValue()
                );
            }
            catch
            {
                return resource;
            }
        }

        [HttpGet("{careGuideId}")]
        [SwaggerOperation(
            Summary = "Get Care Guide by ID",
            Description = "Retrieves a care guide by its unique identifier.",
            OperationId = "GetCareGuideById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Care Guide found!", typeof(CareGuideResource))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Care Guide not found...")]
        public async Task<IActionResult> GetCareGuideById([FromRoute] string careGuideId)
        {
            var getCareGuideByIdQuery = new GetCareGuideByIdQuery(careGuideId);
            var careGuide = await careGuideQueryService.Handle(getCareGuideByIdQuery);
            if (careGuide == null)
            {
                return NotFound($"Care guide with ID {careGuideId} not found...");
            }
            var resource = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(careGuide);
            var enriched = await EnrichWithProductInfo(resource);
            return Ok(enriched);
        }

        [HttpPost("{careGuideId}/file")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(
            Summary = "Upload Care Guide file",
            Description = "Uploads a PDF file and attaches it to the specified care guide.",
            OperationId = "UploadCareGuideFile")]
        [SwaggerResponse(StatusCodes.Status201Created, "File uploaded and care guide updated", typeof(CareGuideResource))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid file format or request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Care Guide not found")]
        public async Task<IActionResult> UploadCareGuideFile([FromRoute] string careGuideId, [FromForm] UploadCareGuideFileResource resource)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            if (string.IsNullOrWhiteSpace(careGuideId)) return BadRequest("careGuideId is required.");
            if (resource?.File == null) return BadRequest("File is required.");

            var isPdf = string.Equals(resource.File.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase) ||
                        (resource.File.FileName?.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ?? false);
            if (!isPdf)
            {
                return BadRequest("Invalid file format. Only PDF is allowed.");
            }
            const long maxSize = 5 * 1024 * 1024;
            if (resource.File.Length is <= 0 or > maxSize)
            {
                return BadRequest($"Invalid file size. Must be > 0 and <= {maxSize / (1024 * 1024)}MB.");
            }

            byte[] data;
            using (var ms = new MemoryStream())
            {
                await resource.File.CopyToAsync(ms);
                data = ms.ToArray();
            }

            try
            {
                var command = new UploadCareGuideFileCommand(careGuideId, resource.File.FileName ?? throw new InvalidOperationException(), resource.File.ContentType ?? "application/pdf", data);
                var updated = await careGuideCommandService.Handle(command);
                var entity = updated.FirstOrDefault();
                if (entity == null) return StatusCode(StatusCodes.Status500InternalServerError, "Failed to attach file");
                var careGuideResource = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(entity);
                return CreatedAtAction(nameof(GetCareGuideById), new { careGuideId = entity.CareGuideId }, careGuideResource);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                    return NotFound($"Care guide with ID {careGuideId} not found...");
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("{accountId}")]
        [SwaggerOperation(
            Summary = "Create a conservation guide by type of liquor",
            Description = "Creates a new conservation guide by type of liquor.",
            OperationId = "CreateCareGuideByTypeOfLiquor")]
        [SwaggerResponse(StatusCodes.Status201Created, "Care Guide created!", typeof(CareGuideResource))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request...")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Account not found...")]
        public async Task<IActionResult> CreateCareGuideByTypeOfLiquor([FromRoute] string accountId, [FromBody] CreateCareGuideByTypeOfLiquorResource resource)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                return BadRequest("AccountId is required.");
            }
            if (resource is null)
            {
                return BadRequest("Request body is required.");
            }

            var productType = resource.TypeOfLiquor;
            if (!Enum.IsDefined(productType))
            {
                return BadRequest("Invalid TypeOfLiquor value.");
            }

            if (string.IsNullOrWhiteSpace(resource.ProductName))
                return BadRequest("ProductName is required.");

            var finalCareGuideId = Guid.NewGuid().ToString();

            var (storagePlace, recommendation) = productType switch
            {
                EProductTypes.Apples or EProductTypes.Pears or EProductTypes.Grapes =>
                    ("Cool, ventilated place", $"Store {productType} in a cool place away from direct sunlight."),
                EProductTypes.Bananas or EProductTypes.Mangos or EProductTypes.Pineapples or EProductTypes.Avocados =>
                    ("Room temperature", $"Keep {productType} at room temperature until ripe, then refrigerate if needed."),
                EProductTypes.Strawberries or EProductTypes.Blueberries or EProductTypes.Kiwis =>
                    ("Refrigerator", $"Store {productType} refrigerated and handle gently."),
                EProductTypes.Watermelons or EProductTypes.Oranges =>
                    ("Cool, dry place", $"Store {productType} in a cool, dry place."),
                _ => ("Cool, dry place", $"Store {productType} in a cool, dry place.")
            };

            var hasProductLink = false;
            string? resolvedProductId = null;
            string? resolvedImageUrl = null;
            var resolved = await TryResolveProductByNameAsync(accountId, resource.ProductName);
            if (resolved.HasValue)
            {
                hasProductLink = true;
                resolvedProductId = resolved.Value.ProductId;
                resolvedImageUrl = resolved.Value.ImageUrl;
            }
            IEnumerable<CareGuide> careGuides;
            if (hasProductLink)
            {
                var createWithProduct = new CreateCareGuideCommand(
                    careGuideId: finalCareGuideId,
                    accountId: accountId,
                    productAssociated: resource.ProductName,
                    productId: resolvedProductId ?? throw new InvalidOperationException(),
                    title: resource.Title,
                    summary: resource.Summary,
                    recommendedMinTemperature: resource.RecommendedMinTemperature,
                    recommendedMaxTemperature: resource.RecommendedMaxTemperature,
                    recommendedPlaceStorage: storagePlace,
                    generalRecommendation: recommendation,
                    imageUrl: resolvedImageUrl
                );
                careGuides = await careGuideCommandService.Handle(createWithProduct);
            }
            else
            {
                var createWithoutProduct = new CreateCareGuideWithoutProductIdCommand(
                    careGuideId: finalCareGuideId,
                    accountId: accountId,
                    productAssociated: resource.ProductName,
                    title: resource.Title,
                    summary: resource.Summary,
                    recommendedMinTemperature: resource.RecommendedMinTemperature,
                    recommendedMaxTemperature: resource.RecommendedMaxTemperature,
                    recommendedPlaceStorage: storagePlace,
                    generalRecommendation: recommendation
                );
                careGuides = await careGuideCommandService.Handle(createWithoutProduct);
            }
            var createdCareGuide = careGuides.FirstOrDefault();
            if (createdCareGuide == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create care guide");
            }
            var careGuideResource = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(createdCareGuide);
            var enrichedCreated = new CareGuideResource(
                CareGuideId: careGuideResource.CareGuideId,
                AccountId: careGuideResource.AccountId,
                ProductId: careGuideResource.ProductId,
                Title: careGuideResource.Title,
                Summary: careGuideResource.Summary,
                RecommendedMinTemperature: careGuideResource.RecommendedMinTemperature,
                RecommendedMaxTemperature: careGuideResource.RecommendedMaxTemperature,
                RecommendedPlaceStorage: careGuideResource.RecommendedPlaceStorage,
                GeneralRecommendation: careGuideResource.GeneralRecommendation,
                TypeOfLiquor: productType.ToString(),
                ProductName: resource.ProductName,
                ImageUrl: hasProductLink ? resolvedImageUrl : null
            );
            return CreatedAtAction(nameof(GetCareGuideById), new { accountId, careGuideId = createdCareGuide.Id }, enrichedCreated);
        }

        [HttpGet("{accountId}/{productType}")]
        [SwaggerOperation(
            Summary = "Get Care Guide by Type of Liquor",
            Description = "Retrieves a care guide by its type of liquor.",  
            OperationId = "GetCareGuideByTypeOfLiquor")]
        [SwaggerResponse(StatusCodes.Status200OK, "Care Guide found!", typeof(CareGuideResource))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Care Guide not found...")]
        public async Task<IActionResult> GetCareGuideByTypeOfLiquor([FromRoute] string accountId, [FromRoute] EProductTypes productType)
        {
            var getCareGuideByTypeOfLiquorQuery = new GetCareGuideByTypeOfLiquorQuery(accountId, productType);
            var careGuide = await careGuideQueryService.Handle(getCareGuideByTypeOfLiquorQuery);
            if (careGuide == null)
            {
                return NotFound($"Care guide for {productType} not found...");
            }
            var resource = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(careGuide);
            var enriched = await EnrichWithProductInfo(resource);
            return Ok(enriched);
        }


        [HttpPut("{careGuideId}")]
        [SwaggerOperation(
            Summary = "Update Care Guide",
            Description = "Update Recommendations for A Specific Care Guide.",
            OperationId = "UpdateCareGuide")]
        [SwaggerResponse(StatusCodes.Status200OK, "Care Guide updated successfully!", typeof(CareGuideResource))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Care Guide could not be updated...")]
        public async Task<IActionResult> UpdateCareGuideRecommendations([FromBody] UpdateCareGuideResource resource, [FromRoute] string careGuideId)
        {
            var updateCareGuideCommand = UpdateCareGuideCommandFromResourceAssembler.ToCommandFromResource(resource, careGuideId);
            var updatedCareGuides = await careGuideCommandService.Handle(updateCareGuideCommand);
            var updatedCareGuide = updatedCareGuides.FirstOrDefault();
            if (updatedCareGuide is null)
            {
                return BadRequest($"Failed to update care guide with ID {careGuideId}. Please check the provided data.");
            }
            var updatedCareGuideResource = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(updatedCareGuide);
            var enrichedUpdate = await EnrichWithProductInfo(updatedCareGuideResource);
            return Ok(enrichedUpdate);
        }

        [HttpDelete("{careGuideId}")]
        [SwaggerOperation(
            OperationId = "DeleteCareGuide")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Care Guide deleted successfully!")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Care Guide not found...")]
        public async Task<IActionResult> DeleteCareGuide([FromRoute] string careGuideId)
        {
            var deleteCareGuideCommand = new DeleteCareGuideCommand(careGuideId);
            await careGuideCommandService.Handle(deleteCareGuideCommand);
            return Ok(new { Message = $"Care Guide with ID {careGuideId} deleted successfully." });
        }

        [HttpPut("{careGuideId}/deallocations")]
        [SwaggerOperation(
            Summary = "Unassing a Care Guide",
            Description = "Unassign a Care Guide From Its Current Product.",
            OperationId = "DeallocateCareGuide")]
        [SwaggerResponse(StatusCodes.Status200OK, "Care Guide unassigned successfully!")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Care Guide could not be unassign...")]
        public async Task<IActionResult> DeallocateCareGuide([FromRoute] string careGuideId)
        {
            var unassignCareGuideCommand = new UnassignCareGuideCommand(careGuideId);
            await careGuideCommandService.Handle(unassignCareGuideCommand);
            return Ok(new
            { Message = $"Care Guide with ID {careGuideId} unassigned from its current product successfully." });
        }

        [HttpPut("{careGuideId}/allocations/{productId}")]
        [SwaggerOperation(
            Summary = "Assign a Care Guide",
            Description = "Assign a Care Guide To a Specific Product.",
            OperationId = "AllocateCareGuideToProduct")]
        [SwaggerResponse(StatusCodes.Status200OK, "Care Guide assigned successfully!")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Care Guide could not be assigned...")]
        public async Task<IActionResult> AllocateCareGuideToProduct([FromRoute] string careGuideId, [FromRoute] string productId)
        {
            var assignCareGuideCommand = new AssignCareGuideToProductCommand(careGuideId, productId);
            await careGuideCommandService.Handle(assignCareGuideCommand);
            return Ok(new
            { Message = $"Care Guide with ID {careGuideId} assigned to product with ID {productId} successfully." });
        }
    }
}

