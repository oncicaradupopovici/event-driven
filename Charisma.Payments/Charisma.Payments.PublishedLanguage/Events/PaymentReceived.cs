using System;
using Charisma.SharedKernel.Messaging.Abstractions;
using Newtonsoft.Json;

namespace Charisma.Payments.PublishedLanguage.Events
{
    public class PaymentReceived : IntegrationEvent
    {
        public Guid PayableId { get; }
        public Guid PaymentId { get; }
        public Guid? InvoiceId { get; }
        public DateTime PaymentDate { get; }

        public PaymentReceived(Guid payableId, Guid paymentId, Guid? invoiceId, DateTime paymentDate)
            : this(payableId, paymentId, invoiceId, paymentDate, Guid.NewGuid(), DateTime.UtcNow)
        {
        }


        [JsonConstructor]
        public PaymentReceived(Guid payableId, Guid paymentId, Guid? invoiceId, DateTime paymentDate, Guid eventId, DateTime creationDate)
            : base(eventId, creationDate)
        {
            PayableId = payableId;
            PaymentId = paymentId;
            InvoiceId = invoiceId;
            PaymentDate = paymentDate;
        }
    }
}
