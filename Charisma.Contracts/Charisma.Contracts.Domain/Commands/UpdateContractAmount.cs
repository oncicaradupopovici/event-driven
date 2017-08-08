using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.Commands
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
