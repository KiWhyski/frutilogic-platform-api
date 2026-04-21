using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services
{
    public interface ICareGuideQueryService
    {
        Task<IEnumerable<CareGuide>> Handle(GetAllCareGuidesByAccountId query);
        Task<CareGuide?> Handle(GetCareGuideByIdQuery query);
        Task<CareGuide?> Handle(GetCareGuideByProductIdQuery query);
        Task<CareGuide?> Handle(GetCareGuideByTypeOfLiquorQuery query);
    }
}

