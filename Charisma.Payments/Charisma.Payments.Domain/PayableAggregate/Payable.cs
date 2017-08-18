using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Payments.Domain.PayableAggregate
{
    public class Payable : EventSourcedAggregateRoot
    {
        public Guid ClientId { get; private set; }
        public decimal Amount { get; private set; }

        public Guid? InvoiceId { get; private set; }

        public Payment Payment { get; private set; }

        public bool IsPayed => Payment != null;


        //needed 4 repository should be private
        public Payable()
        {
        }

        public Payable(Guid clientId, decimal amount, Guid? invoiceId)
        {
            Emit(new PayableCreated(Guid.NewGuid(), Guid.NewGuid(), clientId, amount, invoiceId));
        }

        public void Pay()
        {
            if(this.IsPayed)
                throw new Exception("payment allready payed");

            Emit(new PaymentReceived(Guid.NewGuid(), this.Id, Guid.NewGuid(), this.InvoiceId, DateTime.Now));
        }

        private void Apply(PayableCreated e)
        {
            this.Id = e.AggregateId;
            this.ClientId = e.ClientId;
            this.Amount = e.Amount;
            this.InvoiceId = e.InvoiceId;
        }

        private void Apply(PaymentReceived e)
        {
            this.Payment = new Payment(e.PaymentId, e.PaymentDate, this.Id);
        }
    }

    
}
