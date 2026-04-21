using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Services;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Application.Internal.CommandServices;

/// <summary>
///     Service implementation for handling care guide-related commands.
/// </summary>
/// <param name="careGuideRepository">
///     The repository for handling the CareGuides in the database.
/// </param>
/// <param name="unitOfWork">
///     The unit of work for handling database transactions.
/// </param>
public class CareGuideCommandService(ICareGuideRepository careGuideRepository) : ICareGuideCommandService
{
        /// <summary>
        /// Create a new care guide
        /// </summary>
        /// <param name="command">CreateCareGuideCommand</param>
        /// <returns> The created care guide object.</returns>
        public async Task<IEnumerable<CareGuide>> Handle(CreateCareGuideCommand command)
        {
            var accountId = new AccountId(command.accountId);
            var careGuide = new CareGuide(
                command.careGuideId,
                accountId,
                null,
                command.productId,
                command.title,
                command.summary,
                command.recommendedMinTemperature,
                command.recommendedMaxTemperature,
                command.recommendedPlaceStorage,
                command.generalRecommendation
            );
            careGuide.ProductName = command.productAssociated;
            careGuide.ImageUrl = command.imageUrl;
    
            await careGuideRepository.AddAsync(careGuide);
            return new List<CareGuide> { careGuide };
        }
        
        /// <summary>
        /// Create a new care guide without the product id
        /// </summary>
        /// <param name="command">CreateCareGuideWithoutProductIdCommand</param>
        /// <returns>The created care guide object.</returns>
        public async Task<IEnumerable<CareGuide>> Handle(CreateCareGuideWithoutProductIdCommand command){
            var accountId = new AccountId(command.accountId);
            var careGuide = new CareGuide(
            command.careGuideId,
            accountId,
            null,
            null,
            command.title,
            command.summary,
            command.recommendedMinTemperature,
            command.recommendedMaxTemperature,
            command.recommendedPlaceStorage,
            command.generalRecommendation
            );
            careGuide.ProductName = command.productAssociated;
            
            await careGuideRepository.AddAsync(careGuide);
            return new List<CareGuide> { careGuide };
        }
        
        /// <summary>
        /// Update a care guide
        /// </summary>
        /// <param name="command">UpdateCareGuideCommand</param>
        /// <returns>The updated care guide object.</returns>
        public async Task<IEnumerable<CareGuide>> Handle(UpdateCareGuideCommand command){
            var careGuideToUpdate = await careGuideRepository.GetById(command.careGuideId)?? throw new Exception("CareGuide not found");
            
            careGuideToUpdate.UpdateRecommendations(command.title, command.summary, command.recommendedMinTemperature, command.recommendedMaxTemperature, command.recommendedPlaceStorage, command.generalRecommendation);
            
            await careGuideRepository.UpdateAsync(careGuideToUpdate);
            return new List<CareGuide> { careGuideToUpdate };
        }
        
        /// <summary>
        /// Assign a care guide to a product
        /// </summary>
        /// <param name="command">AssignCareGuideToProductCommand</param>
        /// <returns>The care guide object with the product assigned.</returns>
        public async Task<IEnumerable<CareGuide>> Handle(AssignCareGuideToProductCommand command){
            var careGuideToAssign = await careGuideRepository.GetById(command.careGuideId)?? throw new Exception("CareGuide not found");
            careGuideToAssign.AssignCareGuideToAnotherProduct(command.productId);
            await careGuideRepository.UpdateAsync(careGuideToAssign);
            return new List<CareGuide> { careGuideToAssign };
        }
        
        /// <summary>
        /// Unassign a care guide from a product
        /// </summary>
        /// <param name="command">UnassignCareGuideCommand</param>
        /// <returns>The care guide object with the product unassigned.</returns>
        public async Task<IEnumerable<CareGuide>> Handle(UnassignCareGuideCommand command){
            var careGuideToUnassing = await careGuideRepository.GetById(command.careGuideId)?? throw new Exception("CareGuide not found");
            careGuideToUnassing.UnassignCareGuide();
            await careGuideRepository.UpdateAsync(careGuideToUnassing);
            return new List<CareGuide> { careGuideToUnassing };
        }
        
        /// <summary>
        /// Delete a care guide
        /// </summary>
        /// <param name="command">DeleteCareGuideCommand</param>
        public async Task Handle(DeleteCareGuideCommand command){
            var careGuideToDelete = await careGuideRepository.GetById(command.careGuideId)?? throw new Exception("CareGuide not found");
            await careGuideRepository.DeleteAsync(careGuideToDelete.Id.ToString());
        }
        /// <summary>
        /// Upload/Attach a file to an existing care guide
        /// </summary>
        /// <param name="command">UploadCareGuideFileCommand</param>
        /// <returns>The updated care guide with file metadata.</returns>
        public async Task<IEnumerable<CareGuide>> Handle(UploadCareGuideFileCommand command)
        {
            var careGuide = await careGuideRepository.GetById(command.careGuideId) ?? throw new Exception("CareGuide not found");
            careGuide.AttachFile(command.fileName, command.contentType, command.data);
            await careGuideRepository.UpdateAsync(careGuide);
            return new List<CareGuide> { careGuide };
        }
    }

