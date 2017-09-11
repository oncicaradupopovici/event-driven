using Charisma.SharedKernel.Domain;
using System;
using System.Collections.Generic;

namespace Charisma.Contracts.ReadModel.Entities
{
    public class ContractReadModel : ReadModelEntity
    {

        public decimal Amount { get; set; }

        public Guid ClientId { get; set; }

        public int Version { get; set; }

        public List<ContractLineReadModel> ContractLines { get; } = new List<ContractLineReadModel>();

        public bool IsValidated { get; set; }

        private ContractReadModel()
        {
        }
        public ContractReadModel(Guid id, Guid clientId, int version)
        {
            Id = id;
            ClientId = clientId;
            Version = version;

            
        }
    }
}
