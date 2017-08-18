using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.Payments.PublishedLanguage.Events;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Invoices.Application.EventHandlers
{
    public class PaymentEventHandlers :
        IEventHandler<PaymentReceived>
    {
        private readonly ICrudRepository<Invoice> _invoiceRepository;

        public PaymentEventHandlers(ICrudRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task HandleAsync(PaymentReceived @event)
        {
            if(@event.InvoiceId == null)
                return;

            var invoice = await _invoiceRepository.GetSingleAsync(@event.InvoiceId.Value);
            if (invoice != null)
            {
                invoice.MarkAsPayed(@event.PaymentId);
                await _invoiceRepository.UpdateAsync(invoice);
            }
        }
    }
}
