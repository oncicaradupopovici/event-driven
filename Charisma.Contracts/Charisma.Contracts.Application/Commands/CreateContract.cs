using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Contracts.Application.Commands
{
    public class CreateContract : Command
    {
        public decimal Amount { get;}

        public Guid ClientId { get; }

        public CreateContract(Guid id, decimal amount, Guid clientId)
        {
            this.Id = id;
            this.Amount = amount;
            this.ClientId = clientId;
        }
    }
}
