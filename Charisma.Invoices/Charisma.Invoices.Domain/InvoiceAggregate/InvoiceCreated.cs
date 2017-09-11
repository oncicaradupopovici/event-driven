using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Invoices.Domain.InvoiceAggregate
{
    public class InvoiceCreated : DomainEvent
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