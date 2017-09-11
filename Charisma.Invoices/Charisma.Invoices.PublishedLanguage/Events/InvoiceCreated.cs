using Charisma.SharedKernel.Messaging.Abstractions;
using Newtonsoft.Json;
using System;

namespace Charisma.Invoices.PublishedLanguage.Events
{
    public class InvoiceCreated : IntegrationEvent
    {
        public Guid InvoiceId { get; }
        public decimal Amount { get; }

        public Guid ClientId { get; }

        public Guid? ContractId { get; }

        public InvoiceCreated(Guid invoiceId, Guid clientId, Guid? contractId, decimal amount)
            : this(invoiceId, clientId, contractId, amount, Guid.NewGuid(), DateTime.UtcNow)
        {
        }

        [JsonConstructor]
        public InvoiceCreated(Guid invoiceId, Guid clientId, Guid? contractId, decimal amount, Guid eventId, DateTime creationDate)
            : base(eventId, creationDate)
        {
            InvoiceId = invoiceId;
            Amount = amount;
            ClientId = clientId;
            ContractId = contractId;
        }
    }
}