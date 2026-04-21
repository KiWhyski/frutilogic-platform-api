using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;

/// <summary>
///     Query service interface for retrieving plan information.
/// </summary>
public interface IPlanQueryService
{
    /// <summary>
    ///     Method to handle the retrieval of all available plans.
    /// </summary>
    /// <param name="query">
    ///     The query containing the parameters for retrieving all plans.
    /// </param>
    /// <returns>
    ///     A collection of all available plans.
    /// </returns>
    Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query);
}
