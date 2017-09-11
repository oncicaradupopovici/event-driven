using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Payments.Domain.PayableAggregate
{
    public class PayableCreated : DomainEvent
    {
        public PayableCreated(Guid eventId, Guid aggregateId, Guid clientId, decimal amount, Guid? invoiceId)
            : base(eventId, aggregateId)
        {
            this.ClientId = clientId;
            this.Amount = amount;
            this.InvoiceId = invoiceId;
        }

        public Guid? InvoiceId { get; set; }

        public Guid ClientId { get; private set; }
        public decimal Amount { get; private set; }
    }
}
