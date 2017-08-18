using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Payments.Domain.PayableAggregate
{
    public class Payment : Entity
    {
        public DateTime PaymentDate { get; private set; }

        public Guid PayableId { get; private set; }

        public Payment(Guid paymentId, DateTime paymentDate, Guid payableId)
        {
            this.Id = paymentId;
            this.PaymentDate = paymentDate;
            this.PayableId = payableId;
        }
    }
}
