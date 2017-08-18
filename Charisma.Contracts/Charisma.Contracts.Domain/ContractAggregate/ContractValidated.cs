using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    internal class ContractValidated : Event
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
