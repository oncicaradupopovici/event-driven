using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Invoices.Domain.InvoiceAggregate
{
    public class InvoiceAmountUpdated : Event
    {
        public decimal NewAmount { get; }

        public InvoiceAmountUpdated(Guid id, decimal newAmount)
        {
            Id = id;
            NewAmount = newAmount;
        }
    }
}