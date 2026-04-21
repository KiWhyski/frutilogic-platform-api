using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories
{
    /// <summary>
    /// This interface defines the contract for a repository that manages Care Guide aggregates.
    /// </summary>
    public interface ICareGuideRepository : IBaseRepository<CareGuide>
    {
        Task<CareGuide?> GetById(string id);
        Task<IEnumerable<CareGuide>> GetAllByAccountId(string accountId);
        Task<IEnumerable<CareGuide>> GetAllByProductId(string productId);
        Task<CareGuide?> GetByProductType(string accountId, string productType);
        Task UpdateByCareGuideIdAsync(string careGuideId, CareGuide entity);
    }
}

