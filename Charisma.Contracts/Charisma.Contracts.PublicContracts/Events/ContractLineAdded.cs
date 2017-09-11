using System;
using Charisma.SharedKernel.Messaging.Abstractions;
using Newtonsoft.Json;

namespace Charisma.Contracts.PublishedLanguage.Events
{
    public class ContractLineAdded : IntegrationEvent
    {
        public Guid ContractId { get; }

        public string Product { get; }

        public decimal Price { get; }

        public int Quantity { get; }

        public Guid ContractLineId { get; }

        public int Version { get; }

        public ContractLineAdded(Guid contractId, Guid contractLineId, string product, decimal price, int quantity, int version)
            : this(contractId, contractLineId, product, price, quantity, version, Guid.NewGuid(), DateTime.UtcNow)
        {
        }


        [JsonConstructor]
        public ContractLineAdded(Guid contractId, Guid contractLineId, string product, decimal price, int quantity, int version, Guid eventId, DateTime creationDate)
            : base(eventId, creationDate)
        {
            ContractId = contractId;
            ContractLineId = contractLineId;
            Product = product;
            Price = price;
            Quantity = quantity;
            Version = version;
        }
    }
}
