using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    public class Product : ValueObject<Product>
    {
        public string Name { get; }
        
        public decimal Price { get; }

        //Used by ef
        private Product()
        {
        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
