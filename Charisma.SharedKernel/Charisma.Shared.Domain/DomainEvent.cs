using Charisma.SharedKernel.EventDrivenAbstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Charisma.SharedKernel.Domain
{
    public class DomainEvent : IEvent
    {
        public Guid EventId { get; }
        public Guid AggregateId { get; }

        public int Version;

        public DateTime CreationDate { get; }

        public DomainEvent(Guid eventId, Guid aggregateId)
        {
            EventId = eventId;
            AggregateId = aggregateId;
            CreationDate = DateTime.Now;
        }
    }
}
