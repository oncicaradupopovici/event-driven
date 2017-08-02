using System;
using Charisma.SharedKernel.Domain;

namespace Charisma.Invoices.Domain.Commands
{
    public class UpdateInvoiceAmount : Command
    {
        public decimal NewAmount { get; }

        public UpdateInvoiceAmount(Guid id, decimal newAmount)
        {
            this.Id = id;
            this.NewAmount = newAmount;
        }
    }
}
