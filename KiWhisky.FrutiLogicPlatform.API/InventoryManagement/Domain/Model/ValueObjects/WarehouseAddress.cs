using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Persistence.MongoDB.Configuration.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
///     This record class serves as a Value Object for the address of a warehouse.
/// </summary>
[BsonSerializer(typeof(WarehouseAddressSerializer))]
public record WarehouseAddress()
{
    /// <summary>
    ///     The street component of the warehouse address.
    /// </summary>
    public string Street { get; } = string.Empty;
    
    /// <summary>
    ///     The city component of the warehouse address.
    /// </summary>
    public string City { get; } = string.Empty;
    
    /// <summary>
    ///     The district component of the warehouse address.
    /// </summary>
    public string District { get; } = string.Empty;

    /// <summary>
    ///     The postal code component of the warehouse address.
    /// </summary>
    public string PostalCode { get; } = string.Empty;
    
    /// <summary>
    ///     This country component of the warehouse address.
    /// </summary>
    public string Country { get; } = string.Empty;

    /// <summary>
    ///     Default constructor for WarehouseAddress.
    /// </summary>
    /// <param name="street">
    ///     The street component of the warehouse address. Must be a non-empty string.
    /// </param>
    /// <param name="city">
    ///     The city component of the warehouse address. Must be a non-empty string.
    /// </param>
    /// <param name="district">
    ///     The district component of the warehouse address. Must be a non-empty string.
    /// </param>
    /// <param name="postalCode">
    ///     The postal code component of the warehouse address. Must be a non-empty string.
    /// </param>
    /// <param name="country">
    ///     The country component of the warehouse address. Must be a non-empty string.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Returned when any of the provided address fields are null, empty, or consist only of whitespace.
    /// </exception>
    public WarehouseAddress(string street, string city, string district, string postalCode, string country) : this()
    {
        if (!IsAddressValid(street, city, district, postalCode, country))
            throw new ArgumentException("All address fields must be provided and non-empty.");
        
        Street = street;
        City = city;
        District = district;
        PostalCode = postalCode;
        Country = country;
    }
    
    /// <summary>
    ///     The method to validate the address fields.
    /// </summary>
    /// <returns>
    ///     A boolean indicating whether all address fields are valid (non-null, non-empty, and not just whitespace).
    /// </returns>
    private static bool IsAddressValid(string street, string city, string district, string postalCode, string country) =>
        !string.IsNullOrWhiteSpace(street) && 
        !string.IsNullOrWhiteSpace(city) &&
        !string.IsNullOrWhiteSpace(district) &&
        !string.IsNullOrWhiteSpace(postalCode) &&
        !string.IsNullOrWhiteSpace(country);
    
    /// <summary>
    ///     Method to get the full address as a formatted string.
    /// </summary>
    /// <returns>
    ///     The full address in the format: "Street, City, District, PostalCode, Country".
    /// </returns>
    public string GetFullAddress() => $"{Street}, {City}, {District}, {PostalCode}, {Country}";
}
