using System.Net.Mime;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.ACL.Services;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Controllers;


/// <summary>
/// Controller for managing account addresses.
/// </summary>
[ApiController]
[Route("api/v1/accounts/{accountId}/addresses")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Accounts")]
public class AccountAddressesController(IAccountCommandService accountCommandService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Add address to account.",
        Description = "Adds a new address to the specified account.",
        OperationId = "AddAddressToAccount")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Address added successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Account not found.")]
    public async Task<IActionResult> AddAddress(string accountId, [FromBody] AddAddressResource resource)
    {
        try
        {
            var command = new AddAddressToAccountCommand(
                accountId,
                resource.street,
                resource.city,
                resource.state,
                resource.country,
                resource.zipCode
            );
            
            await accountCommandService.Handle(command);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
