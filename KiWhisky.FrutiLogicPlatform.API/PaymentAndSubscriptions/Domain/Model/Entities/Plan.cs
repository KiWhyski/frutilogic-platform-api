using KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Entities;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KiWhisky.FrutiLogicPlatform.API.PaymentAndSubscriptions.Domain.Model.Entities;

/// <summary>
///    Entity that represents a Subscription Plan
/// </summary>
public class Plan(
    EPlanType planType,
    string description,
    EPaymentFrequency paymentFrequency,
    Money planPrice,
    PlanLimits planLimits
    ) : Entity
{
    /// <summary>
    ///     The type of the plan.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public EPlanType PlanType { get; private set; } = planType;
    
    
    /// <summary>
    ///     A brief description of the plan.
    /// </summary>
    public string Description { get; private set; } = description;
    
    /// <summary>
    ///     The payment frequency of the plan.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public EPaymentFrequency PaymentFrequency { get; private set; } = paymentFrequency;
    
    /// <summary>
    ///     The price of the plan.
    /// </summary>
    public Money PlanPrice { get; private set; } = planPrice;
    
    /// <summary>
    ///     The benefits of the plan.
    /// </summary>
    public PlanLimits PlanLimits { get; private set; } = planLimits;
}
