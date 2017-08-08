using System;
using System.Collections.Generic;
using Charisma.SharedKernel.Core;

namespace Charisma.SharedKernel.Domain
{
    public abstract class AggregateRoot
    {
        protected readonly List<Event> _changes = new List<Event>();

        public Guid Id { get; protected set; }
        public int Version { get; internal set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

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
            if (isNew) _changes.Add(@event);
        }
    }
}
