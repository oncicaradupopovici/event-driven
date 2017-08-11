using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.Payments.Domain.PaymentAggregate
{
    public class PaymentCreated : Event
    {
        public PaymentCreated(Guid eventId, Guid aggregateId, Guid clientId, decimal amount, Guid? invoiceId)
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
