using System.Net.Mime;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Controllers;

/// <summary>
///     Controller for handling brand-related requests.
/// </summary>
/// <param name="brandQueryService">
///     The service for handling brand-related queries.
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available endpoints for brands.")]
public class BrandsController(
        IBrandQueryService brandQueryService
    ) : ControllerBase
{
    /// <summary>
    ///     Endpoint to handle the retrieval of all brands.   
    /// </summary>
    /// <returns>
    ///     The list of all brands. 
    /// </returns>
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all brands.",
        Description = "Retrieves a list of all brand names.",
        OperationId = "GetAllBrands")]
    [SwaggerResponse(StatusCodes.Status200OK, "Brands retrieved successfully.", typeof(IEnumerable<BrandResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Brands could not be retrieved.")]
    public async Task<IActionResult> GetAllBrands()
    {
        var getAllBrandsQuery = new GetAllBrandsQuery();
        var brands = await brandQueryService.Handle(getAllBrandsQuery);
        var enumerable = brands as Brand[] ?? brands.ToArray();
        if (enumerable.Length == 0) return NotFound("No brands found.");
        var brandResources = enumerable.Select(BrandResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(brandResources);
    }
}
