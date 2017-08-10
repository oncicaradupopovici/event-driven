using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.SharedKernel.Domain
{
    public class EventedAggregateRoot : AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        protected void AddEvent(Event @event)
        {
            _changes.Add(@event);
        }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }
    }
}
