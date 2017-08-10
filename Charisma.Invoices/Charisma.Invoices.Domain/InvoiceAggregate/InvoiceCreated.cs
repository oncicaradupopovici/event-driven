using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Invoices.Domain.InvoiceAggregate
{
    public class InvoiceCreated : Event
    {
        public decimal Amount { get; }

        public Guid ClientId { get; }

        public Guid? ContractId { get; }

        public InvoiceCreated(Guid id, Guid clientId, Guid? contractId, decimal amount)
        {
            Id = id;
            Amount = amount;
            ClientId = clientId;
            ContractId = contractId;
        }
    }
}