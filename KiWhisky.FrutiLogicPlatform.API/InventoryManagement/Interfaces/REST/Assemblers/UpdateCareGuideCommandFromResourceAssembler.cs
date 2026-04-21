using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Assemblers
{
    /// <summary>
    /// This class is responsible for transforming an UpdateCareGuideResource into an UpdateCareGuideCommand.
    /// </summary>
    public static class UpdateCareGuideCommandFromResourceAssembler
    {
        /// <summary>
        /// Transforms an UpdateCareGuideResource into an UpdateCareGuideCommand.
        /// </summary>
        /// <returns>
        /// The created UpdateCareGuideCommand.
        /// </returns>
        public static UpdateCareGuideCommand ToCommandFromResource(UpdateCareGuideResource resource, string careGuideId) { 
            return new UpdateCareGuideCommand(
                careGuideId,
                resource.Title,
                resource.Summary,
                resource.RecommendedMinTemperature,
                resource.RecommendedMaxTemperature,
                resource.RecommendedPlaceStorage,
                resource.GeneralRecommendation);
        }
    }
}

