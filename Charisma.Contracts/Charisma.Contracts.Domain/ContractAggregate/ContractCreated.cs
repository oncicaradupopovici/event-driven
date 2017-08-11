using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    internal class ContractCreated : Event
    {
        public Guid ClientId { get; }

        public ContractCreated(Guid eventId, Guid aggregateId, Guid clientId)
            : base(eventId, aggregateId)
        {
            ClientId = clientId;
        }
    }
}
