using System;
using Charisma.SharedKernel.Core;

namespace Charisma.Payments.Domain.PayableAggregate
{
    public class PaymentReceived: Event
    {
        public Guid PaymentId { get; }
        public Guid? InvoiceId { get; }
        public DateTime PaymentDate { get; }

        public PaymentReceived(Guid eventId, Guid aggregateId, Guid paymentId, Guid? invoiceId, DateTime paymentDate)
            : base(eventId, aggregateId)
        {
            PaymentId = paymentId;
            InvoiceId = invoiceId;
            PaymentDate = paymentDate;
        }
    }
}
