using Charisma.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.Payments.Domain.PaymentAggregate
{
    public class Payment : EventSourcedAggregateRoot
    {
        public Guid ClientId { get; private set; }
        public decimal Amount { get; private set; }

        public Guid? InvoiceId { get; private set; }

        public bool IsPayed { get; private set; }

        //needed 4 repository should be private
        public Payment()
        {
        }

        public Payment(Guid clientId, decimal amount, Guid? invoiceId)
        {
            Emit(new PaymentCreated(Guid.NewGuid(), Guid.NewGuid(), clientId, amount, invoiceId));
        }

        public void Pay()
        {
            if(this.IsPayed)
                throw new Exception("payment allready payed");

            Emit(new PaymentPayed(Guid.NewGuid(), this.Id));
        }

        private void Apply(PaymentCreated e)
        {
            this.Id = e.AggregateId;
            this.ClientId = e.ClientId;
            this.Amount = e.Amount;
            this.InvoiceId = e.InvoiceId;
        }

        private void Apply(PaymentPayed e)
        {
            this.IsPayed = true;
        }
    }

    
}
