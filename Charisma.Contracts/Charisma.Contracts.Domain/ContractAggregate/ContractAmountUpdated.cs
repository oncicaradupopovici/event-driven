using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    public class ContractAmountUpdated : DomainEvent
    {
        public decimal NewAmount { get; }

        public ContractAmountUpdated(Guid eventId, Guid aggregateId, decimal newAmount)
            :base(eventId, aggregateId)
        {
            NewAmount = newAmount;
        }
    }
}
