using System.Net.Mime;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Assemblers;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Controllers;

/// <summary>
///     Controller for handling business-related requests.
/// </summary>
[ApiController]
[Route("api/v1/accounts/{accountId}/businesses")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Accounts")]
public class AccountBusinessController(IBusinessQueryService businessQueryService,
                                        IBusinessCommandService businessCommandService) : ControllerBase
{
    /// <summary>
    ///     The service for handling business-related queries.
    /// </summary>
    /// <param name="accountId">
    ///     The route parameter representing the unique identifier of the account for which to retrieve a business.
    /// </param>
    /// <returns>
    ///     A 200 OK response with the business details, or a 404 Not Found response if the business could not be found.  
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get business by account ID.",
        Description = "Retrieves a business by its associated account ID.",
        OperationId = "GetBusinessByAccountId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Business returned successfully.", typeof(BusinessResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Business not found.")]
    public async Task<IActionResult> GetBusinessByAccountId([FromRoute] string accountId)
    {
        var getBusinessById = new GetBusinessByAccountIdQuery(accountId);
        var business = await businessQueryService.Handle(getBusinessById);
        var businessResource = BusinessResourceFromEntityAssembler.ToResourceFromEntity(business);
        return Ok(businessResource);   
        
    }

    /// <summary>
    ///     Endpoint to handle the update of a business.
    /// </summary>
    /// <param name="accountId">
    ///     The route parameter representing the unique identifier of the account for which to update a business.
    /// </param>
    /// <param name="resource">
    ///     The request body containing the updated business information.   
    /// </param>
    /// <returns>
    ///     A 200 OK response with the updated business details, or a 404 Not Found response if the business could not be updated.   
    /// </returns>
    [HttpPut]
    [SwaggerOperation(
        Summary = "Update business by account ID.",
        Description = "Updates a business associated with the specified account ID.",
        OperationId = "UpdateBusinessByAccountId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Business updated successfully.", typeof(BusinessResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Business not found.")]
    public async Task<IActionResult> UpdateBusinessByAccountId([FromRoute] string accountId, [FromBody] UpdateBusinessResource resource)
    {
        var updateBusinessCommand = UpdateBusinessCommandFromResourceAssembler.FromCommandToEntity(resource, accountId);
        var business = await businessCommandService.Handle(updateBusinessCommand);
        if (business is null) return NotFound($"Business with ID {accountId} not found.");
        var businessResource = BusinessResourceFromEntityAssembler.ToResourceFromEntity(business);
        return Ok(businessResource);
    }
}
