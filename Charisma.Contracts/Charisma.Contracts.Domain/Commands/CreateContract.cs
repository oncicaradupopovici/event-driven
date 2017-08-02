using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Domain;

namespace Charisma.Contracts.Domain.Commands
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
