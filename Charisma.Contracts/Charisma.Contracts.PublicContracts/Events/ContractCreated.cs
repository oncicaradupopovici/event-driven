using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Contracts.PublicContracts.Events
{
    public class ContractCreated : Event
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
