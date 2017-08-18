using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Invoices.Domain.InvoiceAggregate
{
    public class Invoice : EventedAggregateRoot
    {
        public Guid ClientId { get; private set; }

        public Guid? ContractId { get; private set; }

        public decimal Amount { get; private set; }

        public bool IsPayed => PaymentId.HasValue;

        public Guid? PaymentId { get; private set; }

        //needed 4 repository should be private
        public Invoice()
        {

        }

        public Invoice(Guid clientId, Guid? contractId, decimal amount)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            ClientId = clientId;
            ContractId = contractId;

            AddEvent(new InvoiceCreated(Guid.NewGuid(), Id, clientId, contractId, amount));
        }

        public void MarkAsPayed(Guid paymentId)
        {
            PaymentId = paymentId;
            AddEvent(new InvoicePayed(Guid.NewGuid(), Id, paymentId));
        }

    }
}
