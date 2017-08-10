using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    public class Contract : EventSourcedAggregateRoot
    {
        public decimal Amount { get; private set; }

        public Guid ClientId { get; private set; }

        //needed 4 repository should be private
        public Contract()
        {
        }



        public Contract(Guid id, decimal amount, Guid clientId)
        {
            Emit(new ContractCreated(id, amount, clientId));
        }

        public void UpdateAmount(decimal newAmount)
        {
            Emit(new ContractAmountUpdated(this.Id, newAmount));
        }




        private void Apply(ContractCreated e)
        {
            this.Id = e.Id;
            this.Amount = e.Amount;
            this.ClientId = e.ClientId;
        }

        private void Apply(ContractAmountUpdated e)
        {
            this.Amount = e.NewAmount;
        }
    }
}
