using System;
using Charisma.SharedKernel.Messaging.Abstractions;
using Newtonsoft.Json;

namespace Charisma.Contracts.PublishedLanguage.Events
{
    public class ContractValidated : IntegrationEvent
    {
        public Guid ContractId { get; }
        public Guid ClientId { get; }
        public decimal Amount { get; }

        public ContractValidated(Guid contractId, Guid clientId, decimal amount)
            : this(contractId, clientId, amount, Guid.NewGuid(), DateTime.UtcNow)
        {
        }

        [JsonConstructor]
        public ContractValidated(Guid contractId, Guid clientId, decimal amount, Guid eventId, DateTime creationDate)
            : base(eventId, creationDate)
        {
            ContractId = contractId;
            ClientId = clientId;
            Amount = amount;
        }
    }
}
