using CloudinaryDotNet.Actions;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Events;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.Authentication.Domain.Model.Events;

public class AccountOwnerUserCreatedEvent: IDomainEvent
{
    public int Id { get; set; }
    public Email Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public required AccountId AccountId { get; set; }
    public Role UserRole { get; set; }
    public string UserRoleId { get; set; }
    public DateTime OccurredOn { get; }
}
