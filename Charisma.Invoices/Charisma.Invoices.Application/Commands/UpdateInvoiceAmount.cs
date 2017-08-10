using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Invoices.Application.Commands
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
