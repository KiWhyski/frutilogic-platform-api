using KiWhisky.FrutiLogicPlatform.API.Authentication.Application.Internal.OutboundServices.Token;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.Authentication.Infrastructure.Pipeline.Middleware.Attributes;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using CustomAllowAnonymousAttribute = KiWhisky.FrutiLogicPlatform.API.Authentication.Infrastructure.Pipeline.Middleware.Attributes.AllowAnonymousAttribute;
namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Infrastructure.Pipeline.Middleware.Components;

/**
 * RequestAuthorizationMiddleware is a custom middleware.
 * This middleware is used to authorize requests.
 * It validates a token is included in the request header and that the token is valid.
 * If the token is valid then it sets the user in HttpContext.Items["User"].
 */
public class RequestAuthorizationMiddleware(RequestDelegate next,
    ILogger<RequestAuthorizationMiddleware> logger)
{
    private readonly ILogger<RequestAuthorizationMiddleware> _logger = logger;

     /**
     * InvokeAsync is called by the ASP.NET Core runtime.
     * It is used to authorize requests.
     * It validates a token is included in the request header and that the token is valid.
     * If the token is valid then it sets the user in HttpContext.Items["User"].
     */
    public async Task InvokeAsync(
        HttpContext     context,
        IUserQueryService userQueryService,
        ITokenService     tokenService,
        IProductRepository productRepository,
        IWarehouseRepository warehouseRepository,
        IInventoryRepository inventoryRepository)
    {
        _logger.LogInformation("Entering InvokeAsync");
            
        if (context.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        var requestPath = context.Request.Path.Value ?? string.Empty;
        if (requestPath.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
            requestPath.StartsWith("/health", StringComparison.OrdinalIgnoreCase) ||
            requestPath.Contains("/api/v1/health", StringComparison.OrdinalIgnoreCase) ||
            requestPath.Equals("/", StringComparison.OrdinalIgnoreCase) ||
            requestPath.EndsWith("/sign-in", StringComparison.OrdinalIgnoreCase) ||
            requestPath.EndsWith("/sign-up", StringComparison.OrdinalIgnoreCase) ||
            requestPath.Contains("/users/recovery-code", StringComparison.OrdinalIgnoreCase) ||
            requestPath.Contains("/users/verify-recovery-code", StringComparison.OrdinalIgnoreCase) ||
            requestPath.Contains("/users/reset-password", StringComparison.OrdinalIgnoreCase) ||
            requestPath.Contains("/authentication/google", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        var endpoint  = context.GetEndpoint();
        var hasCustomAllowAnon = endpoint?.Metadata.Any(m => m is CustomAllowAnonymousAttribute) ?? false;
        var hasBuiltInAllowAnon = endpoint?.Metadata.Any(m => m is IAllowAnonymous || m is Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute) ?? false;
        var allowAnon = hasCustomAllowAnon || hasBuiltInAllowAnon;

        _logger.LogInformation("Allow Anonymous = {AllowAnonymous}", allowAnon);
        if (allowAnon)
        {
            await next(context);
            return;
        }
        
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?
            .Split(' ') 
            .Last();

        if (string.IsNullOrWhiteSpace(token))
        {
            _logger.LogWarning("Missing token");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing or invalid token");
            return;
        }
        
        var userId = await tokenService.ValidateToken(token);
        if (userId is null)
        {
            _logger.LogWarning("Invalid token");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token");
            return;
        }
        
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId));
        if (user is null)
        {
            _logger.LogWarning("Token resolved to a user that no longer exists");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "The authenticated user no longer exists."
            });
            return;
        }

        var routeAccountId = context.Request.RouteValues.TryGetValue("accountId", out var routeValue)
            ? routeValue?.ToString()
            : null;

        if (!string.IsNullOrWhiteSpace(routeAccountId) &&
            !string.Equals(routeAccountId, user.AccountId.GetId, StringComparison.Ordinal))
        {
            _logger.LogWarning(
                "User {UserId} attempted to access account {RouteAccountId}",
                user.Id,
                routeAccountId);
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Detail = "You cannot access resources that belong to another account."
            });
            return;
        }

        var controller = context.Request.RouteValues.TryGetValue("controller", out var controllerValue)
            ? controllerValue?.ToString()
            : null;
        var ownsResource = true;

        if (string.Equals(controller, "Products", StringComparison.OrdinalIgnoreCase) &&
            context.Request.RouteValues.TryGetValue("id", out var productRouteValue) &&
            ObjectId.TryParse(productRouteValue?.ToString(), out var productObjectId))
        {
            var product = await productRepository.FindByIdAsync(productObjectId.ToString());
            ownsResource = product is null ||
                string.Equals(product.AccountId.GetId, user.AccountId.GetId, StringComparison.Ordinal);
        }
        else if ((string.Equals(controller, "Warehouses", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(controller, "WarehouseProducts", StringComparison.OrdinalIgnoreCase)) &&
                 context.Request.RouteValues.TryGetValue("warehouseId", out var warehouseRouteValue) &&
                 ObjectId.TryParse(warehouseRouteValue?.ToString(), out var warehouseObjectId))
        {
            var warehouse = await warehouseRepository.FindByIdAsync(warehouseObjectId.ToString());
            ownsResource = warehouse is null ||
                string.Equals(warehouse.AccountId.GetId, user.AccountId.GetId, StringComparison.Ordinal);
        }
        else if (string.Equals(controller, "Inventories", StringComparison.OrdinalIgnoreCase) &&
                 context.Request.RouteValues.TryGetValue("inventoryId", out var inventoryRouteValue) &&
                 ObjectId.TryParse(inventoryRouteValue?.ToString(), out var inventoryObjectId))
        {
            var inventory = await inventoryRepository.FindByIdAsync(inventoryObjectId.ToString());
            if (inventory is not null)
            {
                var product = await productRepository.FindByIdAsync(inventory.ProductId.ToString());
                ownsResource = product is null ||
                    string.Equals(product.AccountId.GetId, user.AccountId.GetId, StringComparison.Ordinal);
            }
        }

        if (!ownsResource)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Detail = "You cannot access resources that belong to another account."
            });
            return;
        }

        _logger.LogInformation("Successful authorization. Setting HttpContext.Items[\"User\"]");
        context.Items["User"] = user;
        _logger.LogInformation("Continuing with Middleware Pipeline");
        // call next middleware
        await next(context);
    }
}

