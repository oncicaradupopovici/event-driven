using System;
using Charisma.SharedKernel.ReadModel;

namespace Charisma.Contracts.ReadModel.Entities
{
    public class ContractReadModel : ReadModelEntity
    {

        public decimal Amount { get; set; }

        public Guid ClientId { get; set; }

        public int Version { get; set; }

        private ContractReadModel()
        {
            
        }
        public ContractReadModel(Guid id, decimal amount, Guid clientId, int version)
        {
            Id = id;
            Amount = amount;
            ClientId = clientId;
            Version = version;
        }
    }
}
