using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Invoices.Application.Commands
{
    public class UpdateInvoiceAmount : Command
    {
        public Guid InvoiceId { get; }
        public decimal NewAmount { get; }

        public UpdateInvoiceAmount(Guid invoiceId, decimal newAmount)
        {
            this.InvoiceId = invoiceId;
            this.NewAmount = newAmount;
        }
    }
}
