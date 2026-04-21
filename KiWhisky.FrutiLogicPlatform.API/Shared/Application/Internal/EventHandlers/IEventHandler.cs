using Cortex.Mediator.Notifications;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Events;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Application.Internal.EventHandlers;

/// <summary>
///     This class serves as a base interface for all event handlers.
/// </summary>
/// <typeparam name="TEvent">
///     The type of event to handle.
/// </typeparam>
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
    
}
