using System.Threading.Tasks;
using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.Contracts.PublishedLanguage.Events;
using Charisma.SharedKernel.EventDrivenAbstractions;

namespace Charisma.Invoices.Application.IntegrationEventHandlers
{
    public class ContractIntegrationEventHandlers :
        IEventHandler<ContractValidated>
    {
        private readonly ICrudRepository<Invoice> _invoiceRepository;

        public ContractIntegrationEventHandlers(ICrudRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task HandleAsync(ContractValidated @event)
        {
            var invoice = new Invoice(@event.ClientId, @event.ContractId, @event.Amount);
            await _invoiceRepository.AddAsync(invoice);
            await _invoiceRepository.SaveChangesAsync();
        }
    }
}
