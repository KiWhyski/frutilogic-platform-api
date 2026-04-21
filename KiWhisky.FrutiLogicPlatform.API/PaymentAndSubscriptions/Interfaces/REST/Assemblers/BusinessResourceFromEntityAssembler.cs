using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Resources;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Interfaces.REST.Assemblers;

/// <summary>
///     Static class to convert Business entity to BusinessResource.
/// </summary>
public class BusinessResourceFromEntityAssembler
{
    /// <summary>
    ///     Method to convert Business entity to BusinessResource.
    /// </summary>
    /// <param name="entity">
    ///     The Business entity to convert.
    /// </param>
    /// <returns>
    ///     A new instance of BusinessResource representing the provided entity.
    /// </returns>
    public static BusinessResource ToResourceFromEntity(Business entity)
    {
        return new BusinessResource(
            entity.BusinessName.Value,
            entity.BusinessEmail?.Value ?? string.Empty,
            entity.Ruc?.Value ?? string.Empty
        );
    }
}
