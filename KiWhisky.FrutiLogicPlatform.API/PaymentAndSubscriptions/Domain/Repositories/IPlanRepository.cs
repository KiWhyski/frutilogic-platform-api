using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Repositories;

/// <summary>
///     Repository interface for managing Plan entities.
/// </summary>
public interface IPlanRepository : IBaseRepository<Plan>
{
    /// <summary>
    ///     Method to seed predefined plans into the database.
    /// </summary>
    /// <returns>
    ///     A confirmation of the seeding operation.
    /// </returns>
    Task SeedPlansAsync();
    
    /// <summary>
    ///     Method to find the warehouse limit for a plan by account ID.
    /// </summary>
    /// <param name="planId">
    ///     The ID of the plan.
    /// </param>
    /// <returns>
    ///     An integer representing the warehouse limit for the plan.
    /// </returns>
    Task<int?> FindPlanWarehouseLimitsByAccountIdAsync(string planId);
    
    /// <summary>
    ///     Method to find the product limit for a plan by account ID.
    /// </summary>
    /// <param name="planId">}
    ///     The ID of the plan.
    /// </param>
    /// <returns>
    ///     An integer representing the product limit for the plan.
    /// </returns>
    Task<int?> FindPlanProductsLimitByAccountIdAsync(string planId);
    
    /// <summary>
    ///     Method to find the user limit for a plan by account ID.
    /// </summary>
    /// <param name="planId">
    ///     The ID of the plan.
    /// </param>
    /// <returns>
    ///     An integer representing the user limit for the plan.
    /// </returns>
    Task<int?> FindPlanUsersLimitByAccountIdAsync(string planId);
}
