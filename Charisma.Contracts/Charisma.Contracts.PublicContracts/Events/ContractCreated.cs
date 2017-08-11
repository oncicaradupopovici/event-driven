using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Contracts.PublishedLanguage.Events
{
    public class ContractCreated : Event
    {

        public Guid ClientId { get; }

        public ContractCreated(Guid eventId, Guid aggregateId, Guid clientId) 
            : base(eventId, aggregateId)
        {
            ClientId = clientId;
        }
    }
}
