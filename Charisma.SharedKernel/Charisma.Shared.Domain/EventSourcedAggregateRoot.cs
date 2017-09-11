using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.Reflection;

namespace Charisma.SharedKernel.Domain
{
    public abstract class EventSourcedAggregateRoot : EventedAggregateRoot, IEventSourcedAggregateRoot
    {
        public int Version { get; internal set; }

        public void LoadFromHistory(IEnumerable<DomainEvent> history)
        {
            foreach (var e in history) ApplyChanges(e, false);
        }

        //https://github.com/d60/Cirqus/wiki/Emit-Apply-Pattern
        protected void Emit(DomainEvent @event)
        {
            ApplyChanges(@event, true);
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyChanges(DomainEvent @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew) AddEvent(@event);
        }
    }
}
