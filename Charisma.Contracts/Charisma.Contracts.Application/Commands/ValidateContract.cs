using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Contracts.Application.Commands
{
    public class ValidateContract : Command
    {
        public Guid ContractId { get; }

        public ValidateContract(Guid contractId)
        {
            this.ContractId = contractId;
        }
    }
}
