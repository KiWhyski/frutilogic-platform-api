using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Queries;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Application.Internal.QueryServices
{
    public class CareGuideQueryService(ICareGuideRepository careGuideRepository) : ICareGuideQueryService
    {
        public async Task<IEnumerable<CareGuide>> Handle(GetAllCareGuidesByAccountId query){
            return await careGuideRepository.GetAllByAccountId(query.AccountId.GetId);
        }
        public async Task<CareGuide?> Handle(GetCareGuideByIdQuery query) {
            return await careGuideRepository.GetById(query.Id);
        }
        public async Task<CareGuide?> Handle(GetCareGuideByProductIdQuery query) {
            var careGuides = await careGuideRepository.GetAllByProductId(query.ProductId);
            return careGuides.FirstOrDefault();
        }
        
        public async Task<CareGuide?> Handle(GetCareGuideByTypeOfLiquorQuery query) {
            return await careGuideRepository.GetByProductType(query.AccountId, query.TypeOfLiquor.ToString());
        }
    }
}

