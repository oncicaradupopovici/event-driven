using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    public class ContractCreated : DomainEvent
    {
        public Guid ClientId { get; }

        public ContractCreated(Guid eventId, Guid aggregateId, Guid clientId)
            : base(eventId, aggregateId)
        {
            ClientId = clientId;
        }
    }
}
