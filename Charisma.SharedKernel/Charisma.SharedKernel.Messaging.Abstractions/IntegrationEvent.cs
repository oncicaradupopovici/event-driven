using Charisma.SharedKernel.EventDrivenAbstractions;
using System;

namespace Charisma.SharedKernel.Messaging.Abstractions
{
    public class IntegrationEvent : IEvent
    {
        public IntegrationEvent(Guid eventId, DateTime creationDate)
        {
            EventId = eventId;
            CreationDate = creationDate;
        }

        public Guid EventId { get; }
        public DateTime CreationDate { get; }
    }
}
