using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Invoices.Domain.InvoiceAggregate
{
    public class InvoicePayed : DomainEvent
    {
        public Guid PaymentId { get; set; }

        public InvoicePayed(Guid eventId, Guid aggregateId, Guid paymentId)
            : base(eventId, aggregateId)
        {
            PaymentId = paymentId;
        }
    }
}