using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers
{
    /// <summary>
    /// This class is responsible for transforming a CreateCareGuideCommandResource to a CreateCareGuideCommand.
    /// </summary>
    public static class CreateCareGuideCommandFromResourceAssembler
    {
        /// <summary>
        /// Transforms a CreateCareGuideWithoutProductIdResource into a CreateCareGuideWithoutProductIdCommand.
        /// </summary>
        /// <returns>
        /// The created CreateCareGuideWithoutProductIdCommand.
        /// </returns>
        public static CreateCareGuideWithoutProductIdCommand ToCommandFromResource(CreateCareGuideWithoutProductIdResource resource, string accountId)
        {
            // Generate a new GUID for careGuideId
            var careGuideId = Guid.NewGuid().ToString();
            
            return new CreateCareGuideWithoutProductIdCommand(
                careGuideId: careGuideId,
                accountId: accountId,
                productAssociated: null,
                title: resource.Title,
                summary: resource.Summary,
                recommendedMinTemperature: resource.RecommendedMinTemperature,
                recommendedMaxTemperature: resource.RecommendedMaxTemperature,
                recommendedPlaceStorage: resource.RecommendedPlaceStorage,
                generalRecommendation: resource.GeneralRecommendation);
        }
    }
}

