using System.Net.Mime;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Assemblers;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Services;
using System.Security.Claims;
using KiWhisky.FrutiLogicPlatform.API.OrderManagement.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Controllers;

/// <summary>
/// Controller for account-specific purchase order operations.
/// </summary>
[ApiController]
[Route("api/v1/accounts/{accountId}/purchase-orders")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Accounts")]
public class AccountPurchaseOrderController(
    IPurchaseOrderCommandService purchaseOrderCommandService,
    IPurchaseOrderQueryService purchaseOrderQueryService,
    IUserQueryService userQueryService,
    ISalesOrderCommandService salesOrderCommandService) : ControllerBase
{
    /// <summary>
    /// Creates a new purchase order for the specified account.
    /// </summary>
    /// <param name="accountId">The account identifier (buyer).</param>
    /// <param name="resource">The resource containing order details and optional delivery address index.</param>
    /// <returns>The created purchase order resource.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new purchase order for an account.",
        Description = "Creates a new purchase order for the specified account. Optionally, you can specify an addressIndex to set a delivery address from the account's saved addresses.",
        OperationId = "CreatePurchaseOrderForAccount")]
    [SwaggerResponse(StatusCodes.Status201Created, "Purchase order created successfully.", typeof(PurchaseOrderResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request, failed creation, or address not found.")]
    public async Task<IActionResult> CreatePurchaseOrder(string accountId, [FromBody] CreatePurchaseOrderForAccountResource resource)
    {
        try
        {
            if (!await OwnsAccountAsync(accountId))
                return Forbid();

            var command = new CreatePurchaseOrderCommand(
                resource.OrderCode, 
                resource.CatalogIdBuyFrom, 
                accountId,
                resource.AddressIndex,
                resource.Items.Select(i => new CreatePurchaseOrderItemCommand(i.ProductId, i.Quantity)).ToList()
            );
            
            var orderId = await purchaseOrderCommandService.Handle(command);
            try
            {
                await salesOrderCommandService.CreateFromPurchaseOrderAsync(orderId.GetId);
            }
            catch
            {
                await purchaseOrderCommandService.Handle(new CancelOrderCommand(orderId.GetId));
                throw;
            }

            var query = new GetPurchaseOrderByIdQuery(orderId.GetId);
            var order = await purchaseOrderQueryService.Handle(query);

            if (order == null)
                return BadRequest("Failed to create purchase order");

            var orderResource = PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(order);
            return StatusCode(StatusCodes.Status201Created, orderResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "An error occurred while creating the purchase order.", details = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves all purchase orders for the specified account.
    /// </summary>
    /// <param name="accountId">The account identifier (buyer).</param>
    /// <returns>A collection of purchase order resources.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all purchase orders by account.",
        Description = "Retrieves all purchase orders for the specified account (buyer).",
        OperationId = "GetPurchaseOrdersByAccount")]
    [SwaggerResponse(StatusCodes.Status200OK, "Purchase orders retrieved successfully.", typeof(IEnumerable<PurchaseOrderResource>))]
    public async Task<IActionResult> GetPurchaseOrdersByAccount(string accountId)
    {
        if (!await OwnsAccountAsync(accountId))
            return Forbid();

        var query = new GetOrdersByBuyerQuery(accountId);
        var orders = await purchaseOrderQueryService.Handle(query);
        var resources = PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(orders);
        return Ok(resources);
    }

    private async Task<bool> OwnsAccountAsync(string accountId)
    {
        var claimAccountId = User.FindFirst("accountId")?.Value ?? User.FindFirst("accid")?.Value;
        if (!string.IsNullOrWhiteSpace(claimAccountId))
            return claimAccountId.Equals(accountId, StringComparison.OrdinalIgnoreCase);

        if (HttpContext.Items["User"] is Authentication.Domain.Model.Aggregates.User middlewareUser)
            return middlewareUser.AccountId?.GetId.Equals(accountId, StringComparison.OrdinalIgnoreCase) == true;

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? User.FindFirst(ClaimTypes.Sid)?.Value
                     ?? User.FindFirst("sid")?.Value;
        if (string.IsNullOrWhiteSpace(userId))
            return false;
        if (userId.Equals(accountId, StringComparison.OrdinalIgnoreCase))
            return true;
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId));
        return user?.AccountId?.GetId.Equals(accountId, StringComparison.OrdinalIgnoreCase) == true;
    }
}
