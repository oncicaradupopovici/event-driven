using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Invoices.PublishedLanguage.Events
{
    public class InvoiceCreated : Event
    {
        public decimal Amount { get; }

        public Guid ClientId { get; }

        public Guid? ContractId { get; }

        public InvoiceCreated(Guid eventId, Guid aggregateId, Guid clientId, Guid? contractId, decimal amount)
            : base(eventId, aggregateId)
        {
            Amount = amount;
            ClientId = clientId;
            ContractId = contractId;
        }
    }
}