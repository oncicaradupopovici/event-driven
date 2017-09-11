using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    public class ContractValidated : DomainEvent
    {
        public Guid ClientId { get; }
        public decimal Amount { get; }

        public ContractValidated(Guid eventId, Guid aggregateId, Guid clientId,  decimal amount)
            :base(eventId, aggregateId)
        {
            ClientId = clientId;
            Amount = amount;
        }
    }
}
