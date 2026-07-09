using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/accounts/{accountId}/alerts")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("Accounts")]
    public class AccountAlertsController(IAlertQueryService alertQueryService) : ControllerBase
    {
        /// <summary>
        /// This method retrieves all alerts associated with a specific account ID.
        /// </summary>
        /// <param name="accountId">
        /// The ID of the account whose alerts are to be retrieved.
        /// </param>
        /// <returns>
        /// The list of alerts that belong to the specified account ID.
        /// </returns>
        [HttpGet]
        [SwaggerOperation(
        Summary = "Get all alerts by account ID",
        Description = "Retrieves all alerts associated with a specific account ID.",
        OperationId = "GetAlertsByAccountId")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all alerts by account ID.", typeof(IEnumerable<AlertResource>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "No alerts found for the specified account.")]
        public async Task<IActionResult> GetAlertsByAccountId([FromRoute] string accountId)
        {
            var getAllAlertsByAccountIdQuery = new GetAllAlertsByAccountIdQuery(accountId);
            var alerts = await alertQueryService.Handle(getAllAlertsByAccountIdQuery);
            var enumerable = alerts.ToList();
            var alertResources = enumerable.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(alertResources);
        }

        [HttpPost("expiration-scan")]
        [SwaggerOperation(
            Summary = "Generate expiration alerts",
            Description = "Scans account inventories and idempotently creates alerts for upcoming expirations.",
            OperationId = "GenerateExpirationAlerts")]
        public async Task<IActionResult> GenerateExpirationAlerts(
            [FromRoute] string accountId,
            [FromQuery] int daysAhead = 7)
        {
            if (daysAhead is < 1 or > 30)
                return BadRequest("daysAhead must be between 1 and 30.");

            var generated = await alertQueryService.Handle(new GenerateExpirationAlertsQuery(accountId, daysAhead));
            return Ok(new { Generated = generated, DaysAhead = daysAhead });
        }
    }
}

