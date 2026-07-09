using System.Net.Mime;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Assemblers;
using KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KiWhisky.FrutiLogicPlatform.API.ProcurementOrdering.Interfaces.REST.Controllers;

/// <summary>
/// Controller for purchase order operations.
/// </summary>
[ApiController]
[Route("api/v1/purchase-orders")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available endpoints for managing purchase orders.")]
public class PurchaseOrderController(
    IPurchaseOrderCommandService purchaseOrderCommandService,
    IPurchaseOrderQueryService purchaseOrderQueryService,
    IUserQueryService userQueryService) : ControllerBase
{
    [HttpGet("{purchaseOrderId}")]
    [SwaggerOperation(
        Summary = "Get purchase order by ID.",
        Description = "Retrieves a purchase order by its unique identifier.",
        OperationId = "GetPurchaseOrderById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Purchase order returned successfully.", typeof(PurchaseOrderResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchase order not found.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
    public async Task<IActionResult> GetPurchaseOrderById(string purchaseOrderId)
    {
        try
        {
            var query = new GetPurchaseOrderByIdQuery(purchaseOrderId);
            var order = await purchaseOrderQueryService.Handle(query);

            if (order == null)
                return NotFound(new { message = $"Purchase order with ID {purchaseOrderId} not found" });

            var resource = PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(order);
            return Ok(resource);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all purchase orders.",
        Description = "Retrieves all purchase orders in the system.",
        OperationId = "GetAllPurchaseOrders")]
    [SwaggerResponse(StatusCodes.Status200OK, "Purchase orders retrieved successfully.", typeof(IEnumerable<PurchaseOrderResource>))]
    public async Task<IActionResult> GetAllPurchaseOrders()
    {
        var query = new GetAllPurchaseOrdersQuery();
        var orders = await purchaseOrderQueryService.Handle(query);
        var resources = PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(orders);
        return Ok(resources);
    }

    [HttpPost("{purchaseOrderId}/items")]
    [SwaggerOperation(
        Summary = "Add item to purchase order.",
        Description = "Adds a product item to an existing purchase order.",
        OperationId = "AddItemToPurchaseOrder")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Item added successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchase order not found.")]
    public async Task<IActionResult> AddItemToOrder(string purchaseOrderId, [FromBody] AddItemToOrderResource resource)
    {
        try
        {
            if (!await OwnsOrderAsync(purchaseOrderId)) return Forbid();
            var quantity = resource.quantity ?? 1;
            var command = new AddItemToOrderCommand(purchaseOrderId, resource.productId, quantity);
            await purchaseOrderCommandService.Handle(command);
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

    [HttpDelete("{purchaseOrderId}/items/{productId}")]
    [SwaggerOperation(
        Summary = "Remove item from purchase order.",
        Description = "Removes a product item from an existing purchase order.",
        OperationId = "RemoveItemFromPurchaseOrder")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Item removed successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchase order or item not found.")]
    public async Task<IActionResult> RemoveItemFromOrder(string purchaseOrderId, string productId)
    {
        try
        {
            if (!await OwnsOrderAsync(purchaseOrderId)) return Forbid();
            var command = new RemoveItemFromOrderCommand(purchaseOrderId, productId);
            await purchaseOrderCommandService.Handle(command);
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

    [HttpPut("{purchaseOrderId}/confirmations")]
    [SwaggerOperation(
        Summary = "Confirm purchase order.",
        Description = "Confirms an existing purchase order.",
        OperationId = "ConfirmPurchaseOrder")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order confirmed successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchase order not found or cannot be confirmed.")]
    public async Task<IActionResult> ConfirmOrder(string purchaseOrderId)
    {
        try
        {
            if (!await OwnsOrderAsync(purchaseOrderId)) return Forbid();
            var command = new ConfirmOrderCommand(purchaseOrderId);
            await purchaseOrderCommandService.Handle(command);
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

    [HttpPut("{purchaseOrderId}/shipments")]
    [SwaggerOperation(
        Summary = "Ship purchase order.",
        Description = "Marks a purchase order as shipped.",
        OperationId = "ShipPurchaseOrder")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order shipped successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchase order not found or cannot be shipped.")]
    public async Task<IActionResult> ShipOrder(string purchaseOrderId)
    {
        try
        {
            if (!await OwnsOrderAsync(purchaseOrderId)) return Forbid();
            var command = new ShipOrderCommand(purchaseOrderId);
            await purchaseOrderCommandService.Handle(command);
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

    [HttpPut("{purchaseOrderId}/receptions")]
    [SwaggerOperation(
        Summary = "Receive purchase order.",
        Description = "Marks a purchase order as received.",
        OperationId = "ReceivePurchaseOrder")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order received successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchase order not found or cannot be received.")]
    public async Task<IActionResult> ReceiveOrder(string purchaseOrderId)
    {
        try
        {
            if (!await OwnsOrderAsync(purchaseOrderId)) return Forbid();
            var command = new ReceiveOrderCommand(purchaseOrderId);
            await purchaseOrderCommandService.Handle(command);
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

    [HttpPut("{purchaseOrderId}/cancellations")]
    [SwaggerOperation(
        Summary = "Cancel purchase order.",
        Description = "Cancels an existing purchase order.",
        OperationId = "CancelPurchaseOrder")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order canceled successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchase order not found or cannot be canceled.")]
    public async Task<IActionResult> CancelOrder(string purchaseOrderId)
    {
        try
        {
            if (!await OwnsOrderAsync(purchaseOrderId)) return Forbid();
            var command = new CancelOrderCommand(purchaseOrderId);
            await purchaseOrderCommandService.Handle(command);
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

    private async Task<bool> OwnsOrderAsync(string purchaseOrderId)
    {
        var accountId = User.FindFirst("accountId")?.Value ?? User.FindFirst("accid")?.Value;
        if (string.IsNullOrWhiteSpace(accountId))
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sid")?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                return false;
            var user = await userQueryService.Handle(new GetUserByIdQuery(userId));
            accountId = user?.AccountId?.GetId ?? userId;
        }

        var order = await purchaseOrderQueryService.Handle(new GetPurchaseOrderByIdQuery(purchaseOrderId));
        return order != null && order.Buyer.GetId.Equals(accountId, StringComparison.OrdinalIgnoreCase);
    }
}
