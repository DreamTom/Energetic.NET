using Energetic.NET.SharedKernel.IModels;
using MediatR;

namespace Energetic.NET.SharedKernel.BaseModels
{
    public abstract class BaseEntity : IEntity, IDomainEvent
    {
        private readonly List<INotification> _domainEvents = [];

        public long Id { get; private set; }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void AddDomainEventIfAbsent(INotification eventItem)
        {
            if(!_domainEvents.Contains(eventItem))
                _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public IEnumerable<INotification> GetDomainEvents()
        {
            return _domainEvents;
        }

        public void SetId(long id)
        {
            Id = id;
        }
    }
}
