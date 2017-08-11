using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Contracts.Application.Commands
{
    public class CreateContract : Command
    {
        public Guid ClientId { get; }

        public CreateContract(Guid clientId)
        {
            this.ClientId = clientId;
        }
    }
}
