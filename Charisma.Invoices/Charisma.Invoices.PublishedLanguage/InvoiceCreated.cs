using Charisma.SharedKernel.Core;
using System;

namespace Charisma.Invoices.PublishedLanguage
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