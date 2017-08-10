using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    internal class ContractAmountUpdated : Event
    {
        public decimal NewAmount { get; }

        public ContractAmountUpdated(Guid id, decimal newAmount)
        {
            Id = id;
            NewAmount = newAmount;
        }
    }
}
