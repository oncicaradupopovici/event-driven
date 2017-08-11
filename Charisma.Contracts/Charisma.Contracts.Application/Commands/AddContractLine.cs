using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Contracts.Application.Commands
{
    public class AddContractLine : Command
    {

        public string Product { get; }

        public decimal Price { get; }

        public int Quantity { get; }

        public Guid ContractId { get; }


        public AddContractLine(string product, decimal price, int quantity, Guid contractId)
        {
            this.Product = product;
            this.Price = price;
            this.Quantity = quantity;
            this.ContractId = contractId;
        }
    }
}
