using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Entities;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates
{
    /// <summary>
    /// This class represents a Product Care Guide in the domain model.
    /// </summary>
    public class CareGuide : Entity
    {
        public string CareGuideId { get; set; }
        public AccountId AccountId { get; set; }
        public Product ProductAssociated { get; set; }
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ImageUrl { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public double RecommendedMinTemperature { get; set; }
        public double RecommendedMaxTemperature { get; set; }
        public string RecommendedPlaceStorage { get; set; }
        public string GeneralRecommendation { get; set; }
        public string? GuideFileName { get; set; } 
        public string? FileName { get; set; }
        public string? FileContentType { get; set; }
        public byte[]? FileData { get; set; }

        ///<summary>
        /// This constructor is used to create a new care guide.
        /// </summary>
        /// <param name="careGuideId"> The id of the care guide. </param>
        /// <param name="accountId"> The id of the account. </param>
        /// <param name="productAssociated"> The product associated with the care guide. </param>
        /// <param name="productId"> The id of the product. </param>
        /// <param name="title"> The title of the care guide. </param>
        /// <param name="summary"> The summary of the care guide. </param>
        /// <param name="recommendedMinTemperature"> The recommended minimum temperature. </param>
        /// <param name="recommendedMaxTemperature"> The recommended maximum temperature. </param>
        /// <param name="recommendedPlaceStorage"> The recommended place storage. </param>
        /// <param name="generalRecommendation"> The general recommendation. </param>
        public CareGuide(string careGuideId, AccountId accountId, Product productAssociated, string productId, string title, string summary, double recommendedMinTemperature, double recommendedMaxTemperature, string recommendedPlaceStorage, string generalRecommendation)
        {
            CareGuideId = careGuideId;
            AccountId = accountId;
            ProductAssociated = productAssociated;
            ProductId = productId;
            ProductName = productAssociated?.Name;
            Title = title;
            Summary = summary;
            RecommendedMinTemperature = recommendedMinTemperature;
            RecommendedMaxTemperature = recommendedMaxTemperature;
            RecommendedPlaceStorage = recommendedPlaceStorage;
            GeneralRecommendation = generalRecommendation;
        }

        ///<summary>
        /// This constructor is used to create a new care guide.
        /// </summary>
        /// <param name="accountId"> The id of the account. </param>
        /// <param name="productAssociated"> The product associated with the care guide. </param>
        /// <param name="title"> The title of the care guide. </param>
        /// <param name="summary"> The summary of the care guide. </param>
        /// <param name="recommendedMinTemperature"> The recommended minimum temperature. </param>
        /// <param name="recommendedMaxTemperature"> The recommended maximum temperature. </param>
        /// <param name="recommendedPlaceStorage"> The recommended place storage. </param>
        /// <param name="generalRecommendation"> The general recommendation. </param>
        public CareGuide(AccountId accountId, Product productAssociated, string title, string summary, double recommendedMinTemperature, double recommendedMaxTemperature, string recommendedPlaceStorage, string generalRecommendation) 
        { 
            if(string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be null or empty.");
            if(string.IsNullOrEmpty(summary))
                throw new ArgumentException("Summary cannot be null or empty.");
            if(string.IsNullOrEmpty(recommendedPlaceStorage))
                throw new ArgumentException("Recommended place storage cannot be null or empty.");
            if(string.IsNullOrEmpty(generalRecommendation))
                throw new ArgumentException("General recommendation cannot be null or empty.");
            CareGuideId = Guid.NewGuid().ToString();
            AccountId = accountId;
            ProductAssociated = productAssociated;
            ProductId = null;
            ProductName = productAssociated?.Name;
            Title = title;
            Summary = summary;
            RecommendedMinTemperature = recommendedMinTemperature;
            RecommendedMaxTemperature = recommendedMaxTemperature;
            RecommendedPlaceStorage = recommendedPlaceStorage;
            GeneralRecommendation = generalRecommendation;
        }
        ///<summary>
        /// This method is used to update the recommendations of the care guide.
        /// </summary>
        public void UpdateRecommendations(string title, string summary, double recommendedMinTemperature, double recommendedMaxTemperature, string recommendedPlaceStorage, string generalRecommendation) { 
            Title = title;
            Summary = summary;
            RecommendedMinTemperature = recommendedMinTemperature;
            RecommendedMaxTemperature = recommendedMaxTemperature;
            RecommendedPlaceStorage = recommendedPlaceStorage;
            GeneralRecommendation = generalRecommendation;
        }
         /// <summary>
        /// This method is used to unassign the care guide of the current product.
        /// </summary>
        public void UnassignCareGuide()
        {
            ProductId = null;
            ProductAssociated = null;
        }
    
        /// <summary>
        /// Method for assigning this care guide to another product.
        /// </summary>
        /// <param name="newProductId"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AssignCareGuideToAnotherProduct(string newProductId)
        {
            if (newProductId == ProductId)
            {
                throw new ArgumentException("Cannot assign a care guide to the same product.");
            }
            else
            {
                ProductId = newProductId;
                ProductAssociated = null;
            }
        }
        /// <summary>
        /// Attach a binary file to this care guide.
        /// </summary>
        /// <param name="fileName">Original file name</param>
        /// <param name="contentType">MIME type</param>
        /// <param name="data">Binary data</param>
        public void AttachFile(string fileName, string contentType, byte[] data)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("fileName is required");
            if (string.IsNullOrWhiteSpace(contentType)) throw new ArgumentException("contentType is required");
            if (data == null || data.Length == 0) throw new ArgumentException("file data is empty");

            GuideFileName = fileName; 
            FileName = fileName;
            FileContentType = contentType;
            FileData = data;
        }
        public CareGuide() { }

    }
}

