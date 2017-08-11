using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Contracts.PublishedLanguage.Events
{
    public class ContractAmountUpdated : Event
    {
        public decimal NewAmount { get; }

        public ContractAmountUpdated(Guid eventId, Guid aggregateId, decimal newAmount)
            : base(eventId, aggregateId)
        {
            NewAmount = newAmount;
        }
    }
}
