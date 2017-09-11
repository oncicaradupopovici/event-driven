using Charisma.SharedKernel.Messaging.Abstractions;
using Newtonsoft.Json;
using System;

namespace Charisma.Contracts.PublishedLanguage.Events
{
    public class ContractCreated : IntegrationEvent
    {
        public Guid ContractId { get; }
        public Guid ClientId { get; }

        public int Version { get; }

        public ContractCreated(Guid contractId, Guid clientId, int version)
            : this(contractId, clientId, version, Guid.NewGuid(), DateTime.UtcNow)
        {
        }

        [JsonConstructor]
        public ContractCreated(Guid contractId, Guid clientId, int version, Guid eventId, DateTime creationDate)
            : base(eventId, creationDate)
        {
            ContractId = contractId;
            ClientId = clientId;
            Version = version;
        }
    }
}
