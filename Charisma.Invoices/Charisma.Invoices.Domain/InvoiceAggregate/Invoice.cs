using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Invoices.Domain.InvoiceAggregate
{
    public class Invoice : EventedAggregateRoot
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
            Id = id;
            Amount = amount;
            ClientId = clientId;
            ContractId = contractId;

            AddEvent(new InvoiceCreated(id, clientId, contractId, amount));
        }

        public void UpdateAmount(decimal newAmount)
        {
            this.Amount = newAmount;

            AddEvent(new InvoiceAmountUpdated(this.Id, newAmount));
        }

    }
}
