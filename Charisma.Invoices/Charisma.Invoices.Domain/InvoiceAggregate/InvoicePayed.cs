using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Invoices.Domain.InvoiceAggregate
{
    public class InvoicePayed : Event
    {
        public Guid PaymentId { get; set; }

        public InvoicePayed(Guid eventId, Guid aggregateId, Guid paymentId)
            : base(eventId, aggregateId)
        {
            PaymentId = paymentId;
        }
    }
}