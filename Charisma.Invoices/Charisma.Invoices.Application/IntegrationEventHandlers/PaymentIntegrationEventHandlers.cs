using System.Threading.Tasks;
using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.Payments.PublishedLanguage.Events;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.EventDrivenAbstractions;

namespace Charisma.Invoices.Application.IntegrationEventHandlers
{
    public class PaymentIntegrationEventHandlers :
        IEventHandler<PaymentReceived>
    {
        private readonly ICrudRepository<Invoice> _invoiceRepository;

        public PaymentIntegrationEventHandlers(ICrudRepository<Invoice> invoiceRepository)
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
                await _invoiceRepository.SaveChangesAsync();
            }
        }
    }
}
