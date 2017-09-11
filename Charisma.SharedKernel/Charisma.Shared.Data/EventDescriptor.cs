using Charisma.SharedKernel.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Charisma.SharedKernel.Data.Abstractions
{
    public class EventDescriptor
    {
        public int EventId { get; private set; }

        public DomainEvent EventData => JsonConvert.DeserializeObject(JsonEventData, Type.GetType(EventType)) as DomainEvent;

        public string JsonEventData { get; private set; }

        public string EventType { get; private set; }

        public Guid AggregateId { get; private set; }
        public int Version { get; private set; }

        public DateTime CreationDate { get; private set; }

        private EventDescriptor() { }

        public EventDescriptor(Guid aggregateId, DomainEvent eventData, int version, DateTime creationDate)
        {
            //EventData = eventData;
            JsonEventData = JsonConvert.SerializeObject(eventData);
            EventType = eventData.GetType().AssemblyQualifiedName;
            Version = version;
            AggregateId = aggregateId;
            CreationDate = creationDate;
        }
    }
}
