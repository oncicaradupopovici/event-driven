using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.SharedKernel.Domain
{
    public abstract class EventSourcedAggregateRoot : EventedAggregateRoot
    {
        public int Version { get; internal set; }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChanges(e, false);
        }

        //https://github.com/d60/Cirqus/wiki/Emit-Apply-Pattern
        protected void Emit(Event @event)
        {
            ApplyChanges(@event, true);
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyChanges(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew) AddEvent(@event);
        }
    }
}
