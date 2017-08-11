using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    public class ContractLine : Entity
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public Guid ContractId { get; private set; }


        //needed by ef
        //private ContractLine()
        //{
        //}

        public ContractLine(Product product, int quantity, Guid contractId)
        {
            Product = product;
            Quantity = quantity;
            ContractId = contractId;
        }
    }
}
