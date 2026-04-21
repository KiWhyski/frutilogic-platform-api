using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Queries;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;

/// <summary>
///     Query service interface for retrieving business information.
/// </summary>
public interface IBusinessQueryService
{
    /// <summary>
    ///     Method to handle the retrieval of a business by its associated account ID.
    /// </summary>
    /// <param name="query">
    ///     The query object containing the account ID.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    /// </returns>
    Task<Business> Handle(GetBusinessByAccountIdQuery query);
}
