using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

/// <summary>
///     This record class serves as a Value Object for an image URL.
/// </summary>
[BsonSerializer(typeof(ImageUrlSerializer))]
public record ImageUrl()
{
    /// <summary>
    ///     The image URL for a profile, product or warehouse.
    /// </summary>
    private Uri? Value { get; set; } = null;
    
    /// <summary>
    ///     The constructor initializes a new instance of the ImageUrl class with a default image URL.
    /// </summary>
    /// <param name="imageUri">
    ///     The image url
    /// </param>
    public ImageUrl(string imageUri) : this()
    {
        if (string.IsNullOrWhiteSpace(imageUri))
        {
            throw new ValueObjectValidationException(nameof(ImageUrl), "Trying to use a image url null or empty.");
        }

        Value = CreateValidateUrl(imageUri);
    }
    
    /// <summary>
    ///     Validates and creates a URI from the provided image URL string.
    /// </summary>
    /// <param name="imageUri">
    ///     The image Url
    /// </param>
    /// <returns>
    ///     The image uri result
    /// </returns>
    /// <exception cref="ValueObjectValidationException">
    ///     Validates if the image url can be in the https protocol
    /// </exception>
    private static Uri CreateValidateUrl(string imageUri)
    {
        if (!Uri.TryCreate(imageUri, UriKind.Absolute, out var uriResult))
        {
            throw new ValueObjectValidationException(nameof(ImageUrl), "Couldn't create the uri.");
        }
        
        return uriResult.Scheme is not ("https" or "http")
            ? throw new ValueObjectValidationException(nameof(ImageUrl), "Image URL must use http or https.")
            : uriResult;
    }
    
    /// <summary>
    ///     This method returns the image URL.
    /// </summary>
    /// <returns></returns>
    public string GetValue() => Value?.AbsoluteUri ?? string.Empty;
}
