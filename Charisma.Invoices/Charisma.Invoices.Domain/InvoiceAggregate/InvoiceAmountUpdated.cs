using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Invoices.Domain.InvoiceAggregate
{
    public class InvoiceAmountUpdated : Event
    {
        public decimal NewAmount { get; }

        public InvoiceAmountUpdated(Guid eventId, Guid aggregateId, decimal newAmount)
            :base(eventId, aggregateId)
        {
            NewAmount = newAmount;
        }
    }
}