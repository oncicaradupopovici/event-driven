using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.ReadModel.Entities
{
    public class ContractLineReadModel : ReadModelEntity
    {

        public string Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Guid ContractId { get; set; }



        private ContractLineReadModel()
        {
            
        }

        public ContractLineReadModel(Guid id, string product, decimal price, int quantity, Guid contractId)
        {
            Id = id;
            Product = product;
            Price = price;
            Quantity = quantity;
            ContractId = contractId;
        }
    }
}
