using Charisma.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Charisma.Invoices.Domain.Events;

namespace Charisma.Invoices.Domain.Aggregates
{
    public class Invoice : AggregateRoot
    {
        public Guid ClientId { get; private set; }

        public Guid? ContractId { get; private set; }

        public decimal Amount { get; private set; }

        //needed 4 repository should be private
        public Invoice()
        {

        }

        public Invoice(Guid id, Guid clientId, Guid? contractId, decimal amount)
        {
            ApplyChange(new InvoiceCreated(id, clientId, contractId, amount));
        }

        public void UpdateAmount(decimal newAmount)
        {
            ApplyChange(new InvoiceAmountUpdated(this.Id, newAmount));
        }



        private void Apply(InvoiceCreated e)
        {
            this.Id = e.Id;
            this.Amount = e.Amount;
            this.ClientId = e.ClientId;
            this.ContractId = e.ContractId;
        }

        private void Apply(InvoiceAmountUpdated e)
        {
            this.Amount = e.NewAmount;
        }
    }
}
