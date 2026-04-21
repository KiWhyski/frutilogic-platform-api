using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Commands;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Services;

/// <summary>
///     Interface for handling business-related commands.
/// </summary>
public interface IBusinessCommandService
{
    /// <summary>
    ///     Method to handle the creation of a new business.  
    /// </summary>
    /// <param name="command">
    ///     The command containing the details for creating a new business. 
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the newly created business.
    /// </returns>
    Task<Business?> Handle(CreateBusinessCommand command);
    
    /// <summary>
    ///     Method to handle the update of an existing business. 
    /// </summary>
    /// <param name="command">
    ///     The command containing the details for updating an existing business.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the updated business.
    /// </returns>
    Task<Business?> Handle(UpdateBusinessCommand command);
}
