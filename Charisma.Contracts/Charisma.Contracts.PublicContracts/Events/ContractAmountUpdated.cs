using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.PublicContracts.Events
{
    public class ContractAmountUpdated : Event
    {
        public decimal NewAmount { get; }

        public ContractAmountUpdated(Guid id, decimal newAmount)
        {
            Id = id;
            NewAmount = newAmount;
        }
    }
}
