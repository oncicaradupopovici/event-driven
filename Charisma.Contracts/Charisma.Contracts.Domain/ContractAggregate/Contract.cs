using System;
using System.Collections.Generic;
using System.Linq;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    public class Contract : EventSourcedAggregateRoot
    {
        public decimal Amount { get; private set; }

        public Guid ClientId { get; private set; }

        public List<ContractLine> ContractLines { get; private set; }

        public bool IsValidated { get; private set; }


        //needed 4 repository should be private
        public Contract()
        {
        }



        public Contract(Guid clientId)
        {
            Emit(new ContractCreated(Guid.NewGuid(), Guid.NewGuid(), clientId));
        }


        public void AddContractLine(string product, decimal price, int quantity)
        {
            if(IsValidated)
                throw new Exception("Contract is validated");

            Emit(new ContractLineAdded(Guid.NewGuid(), this.Id, Guid.NewGuid(), product, price, quantity));
            Emit(new ContractAmountUpdated(Guid.NewGuid(), this.Id, this.Amount + price * quantity));
        }

        public void Validate()
        {
            if(IsValidated)
                throw new Exception("Contract already validated");
            Emit(new ContractValidated(Guid.NewGuid(), this.Id, this.ClientId, this.Amount));
        }



        private void Apply(ContractCreated e)
        {
            this.Id = e.AggregateId;
            this.ClientId = e.ClientId;
            this.ContractLines = new List<ContractLine>();
        }

        private void Apply(ContractAmountUpdated e)
        {
            this.Amount = e.NewAmount;
        }

        private void Apply(ContractLineAdded e)
        {
            var contractLine = new ContractLine(new Product(e.Product, e.Price), e.Quantity, this.Id);
            this.ContractLines.Add(contractLine);
        }

        private void Apply(ContractValidated e)
        {
            this.IsValidated = true;
        }


    }
}
