using System;
using System.Collections.Generic;
using Charisma.SharedKernel.ReadModel;

namespace Charisma.Contracts.ReadModel.Entities
{
    public class ContractReadModel : ReadModelEntity
    {

        public decimal Amount { get; set; }

        public Guid ClientId { get; set; }

        public int Version { get; set; }

        public List<ContractLineReadModel> ContractLines { get; private set; }

        private ContractReadModel()
        {
            ContractLines = new List<ContractLineReadModel>();
        }
        public ContractReadModel(Guid id, Guid clientId, int version)
        {
            Id = id;
            ClientId = clientId;
            Version = version;

            
        }
    }
}
