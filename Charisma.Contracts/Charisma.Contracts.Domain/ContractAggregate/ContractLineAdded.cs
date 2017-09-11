using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.ContractAggregate
{
    public class ContractLineAdded : DomainEvent
    {
        public string Product { get; }

        public decimal Price { get; }

        public int Quantity { get; }

        public Guid ContractLineId { get; }

        public ContractLineAdded(Guid eventId, Guid aggregateId, Guid contractLineId, string product, decimal price, int quantity)
            : base(eventId, aggregateId)
        {
            ContractLineId = contractLineId;
            Product = product;
            Price = price;
            Quantity = quantity;
        }
    }
}
