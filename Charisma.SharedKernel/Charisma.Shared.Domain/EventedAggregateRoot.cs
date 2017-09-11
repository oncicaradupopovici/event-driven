using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.SharedKernel.Domain
{
    public abstract class EventedAggregateRoot : Entity, IEventedAggregateRoot
    {
        private readonly List<DomainEvent> _changes = new List<DomainEvent>();

        protected void AddEvent(DomainEvent @event)
        {
            _changes.Add(@event);
        }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }
    }
}
