using System;
using System.Linq;
using System.Threading.Tasks;
using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.Contracts.PublishedLanguage.Events;

namespace Charisma.Invoices.Application.EventHandlers
{
    public class ContractEventHandlers :
        IEventHandler<ContractValidated>
    {
        private readonly ICrudRepository<Invoice> _invoiceRepository;

        public ContractEventHandlers(ICrudRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public Task HandleAsync(ContractValidated @event)
        {
            var invoice = new Invoice(@event.ClientId, @event.AggregateId, @event.Amount);
            return _invoiceRepository.AddAsync(invoice);
        }
    }
}
