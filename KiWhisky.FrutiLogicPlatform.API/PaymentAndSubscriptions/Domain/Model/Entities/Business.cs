using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;

/// <summary>
///     Entity that represents a Business.
/// </summary>
public class Business : Entity
{
    
    /// <summary>
    ///     The name of the business.
    /// </summary>
    public BusinessName BusinessName { get; private set; }
    
    /// <summary>
    ///     The main email of the business.
    /// </summary>
    public BusinessEmail? BusinessEmail { get; private set; }
    
    /// <summary>
    ///     The RUC of the business.
    /// </summary>
    public Ruc? Ruc { get; private set; }

    /// <summary>
    ///     Default constructor for Business.
    /// </summary>
    /// <param name="businessName">
    ///     The name of the business.
    /// </param>
    public Business(BusinessName businessName) => BusinessName = businessName;
    
    /// <summary>
    ///     Constructor for Business with all properties.
    /// </summary>
    /// <param name="businessName">
    ///     The name of the business.  
    /// </param>
    /// <param name="businessEmail">
    ///     The main email of the business. 
    /// </param>
    /// <param name="ruc">
    ///     The RUC of the business.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The business name cannot be null.
    /// </exception>
    public Business(BusinessName businessName, BusinessEmail businessEmail, Ruc ruc)
    {
        BusinessName = businessName ?? throw new ArgumentNullException(nameof(businessName));
        BusinessEmail = businessEmail;
        Ruc = ruc;
    }

    /// <summary>
    ///     Method to update the business information.
    /// </summary>
    /// <param name="command">
    ///     The command containing the details for updating the business information.
    /// </param>
    /// <returns>
    ///     A new instance of the Business with the updated information.
    /// </returns>
    public Business UpdateBusiness(UpdateBusinessCommand command)
    {
        BusinessName = new BusinessName(command.BusinessName);
        BusinessEmail = new BusinessEmail(command.BusinessEmail);
        Ruc = new Ruc(command.Ruc);
        return this;
    }
}
