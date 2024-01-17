using MediatR;

namespace Energetic.NET.SharedKernel.IModels
{
    public interface IDomainEvent
    {
        IEnumerable<INotification> GetDomainEvents();

        void AddDomainEvent(INotification eventItem);

        void AddDomainEventIfAbsent(INotification eventItem);

        void ClearDomainEvents();
    }
}
