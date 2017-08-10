using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    internal class ContractCreated : Event
    {
        public decimal Amount { get; }

        public Guid ClientId { get; }
        public ContractCreated(Guid id, decimal amount, Guid clientId)
        {
            Id = id;
            Amount = amount;
            ClientId = clientId;
        }
    }
}
