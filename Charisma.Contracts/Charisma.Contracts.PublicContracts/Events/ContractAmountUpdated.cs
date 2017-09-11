using Charisma.SharedKernel.Messaging.Abstractions;
using Newtonsoft.Json;
using System;

namespace Charisma.Contracts.PublishedLanguage.Events
{
    public class ContractAmountUpdated : IntegrationEvent
    {
        public Guid ContractId { get; }
        public decimal NewAmount { get; }
        public int Version { get; }

        public ContractAmountUpdated(Guid contractId, decimal newAmount, int version)
            : this(contractId, newAmount, version, Guid.NewGuid(), DateTime.UtcNow)
        {
        }

        [JsonConstructor]
        public ContractAmountUpdated(Guid contractId, decimal newAmount, int version, Guid eventId, DateTime creationDate)
            : base(eventId, creationDate)
        {
            ContractId = contractId;
            NewAmount = newAmount;
            Version = version;
        }
    }
}
