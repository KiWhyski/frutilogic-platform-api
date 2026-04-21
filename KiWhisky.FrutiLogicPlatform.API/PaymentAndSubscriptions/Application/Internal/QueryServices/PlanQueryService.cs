using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Application.Internal.QueryServices;

/// <summary>
///     Implementation of the <see cref="IPlanQueryService"/> interface.
/// </summary>
/// <param name="planRepository">
///     The repository for handling plan-related operations.
/// </param>
public class PlanQueryService(IPlanRepository planRepository) : IPlanQueryService
{
    /// <summary>
    ///     Method to handle the retrieval of all available plans.   
    /// </summary>
    /// <param name="query">
    ///     The query object containing parameters for retrieving all plans.
    /// </param>
    /// <returns>
    ///     The list of all available plans.
    /// </returns>
    public async Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query)
    {
        return await planRepository.GetAllAsync();
    }
}
