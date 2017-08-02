using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Invoices.Domain.Events
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
