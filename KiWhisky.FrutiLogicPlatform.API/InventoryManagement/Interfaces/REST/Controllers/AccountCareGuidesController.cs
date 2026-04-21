using System.Net.Mime;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Controllers;

/// <summary>
///     Controller for handling care guide-related requests.
/// </summary>
/// <param name="careGuideQueryService">
///     The service for handling care guide-related queries.
/// </param>
[ApiController]
[Route("api/v1/accounts/{accountId}/care-guides")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Accounts")]
public class AccountCareGuidesController(
        ICareGuideQueryService careGuideQueryService
    ) : ControllerBase
{
    /// <summary>
    ///     Endpoint to handle the retrieval of all care guides for a specific account.
    /// </summary>
    /// <param name="accountId">
    ///     The route parameter representing the unique identifier of the account for which to retrieve care guides.
    /// </param>
    /// <returns>
    ///     A list of care guides for the specified account, or a 404 Not Found response if no care guides are found for the given account ID.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Care Guides by Account ID",
        Description = "Retrieves a list of care guides by a specific Account ID.",
        OperationId = "GetAllCareGuidesByAccountId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Care Guides found!", typeof(List<CareGuideResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Care Guides not found for the give Account ID...")]
    public async Task<IActionResult> GetAllCareGuidesByAccountId([FromRoute] string accountId)
    {
        var targetAccountId = new AccountId(accountId);
        var getAllCareGuidesByAccountIdCommand = new GetAllCareGuidesByAccountId(targetAccountId);
        var careGuides = await careGuideQueryService.Handle(getAllCareGuidesByAccountIdCommand);
        var enumerable = careGuides.ToList();
        if (enumerable.Count == 0)
        {
            return NotFound($"Care guides for Account ID {accountId} not found...");
        }
        var resources = enumerable.Select(CareGuideResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    
}
