using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Invoices.Application.Commands
{
    public class CreateInvoice : Command
    {
        public decimal Amount { get;}

        public Guid ClientId { get; }

        public Guid? ContractId { get; }

        public CreateInvoice(decimal amount, Guid clientId, Guid? contractId)
        {
            this.Amount = amount;
            this.ClientId = clientId;
            this.ContractId = contractId;
        }
    }
}
