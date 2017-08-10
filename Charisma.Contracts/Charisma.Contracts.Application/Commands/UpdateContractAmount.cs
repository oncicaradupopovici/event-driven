using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Contracts.Application.Commands
{
    public class UpdateContractAmount : Command
    {
        public decimal NewAmount { get; }
        public int? OriginalVersion { get; }

        public UpdateContractAmount(Guid id, decimal newAmount, int? originalVersion = null)
        {
            this.Id = id;
            this.NewAmount = newAmount;
            this.OriginalVersion = originalVersion;
        }
    }
}
