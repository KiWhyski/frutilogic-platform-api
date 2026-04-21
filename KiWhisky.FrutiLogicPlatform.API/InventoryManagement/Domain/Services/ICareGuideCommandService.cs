using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services
{
    public interface ICareGuideCommandService
    {
        Task<IEnumerable<CareGuide>> Handle(CreateCareGuideCommand command);
        Task<IEnumerable<CareGuide>> Handle(CreateCareGuideWithoutProductIdCommand command);
        Task<IEnumerable<CareGuide>> Handle(AssignCareGuideToProductCommand command);
        Task<IEnumerable<CareGuide>> Handle(UnassignCareGuideCommand command);
        Task<IEnumerable<CareGuide>> Handle(UpdateCareGuideCommand command);
        Task<IEnumerable<CareGuide>> Handle(UploadCareGuideFileCommand command);
        Task Handle(DeleteCareGuideCommand command);
    }
}

