using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    internal class ContractAmountUpdated : Event
    {
        public decimal NewAmount { get; }

        public ContractAmountUpdated(Guid eventId, Guid aggregateId, decimal newAmount)
            :base(eventId, aggregateId)
        {
            NewAmount = newAmount;
        }
    }
}
