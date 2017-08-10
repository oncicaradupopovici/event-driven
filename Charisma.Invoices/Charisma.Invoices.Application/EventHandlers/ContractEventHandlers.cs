using System;
using System.Linq;
using System.Threading.Tasks;
using Charisma.Contracts.PublicContracts.Events;
using Charisma.Invoices.Domain.Aggregates;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Invoices.Application.EventHandlers
{
    public class ContractEventHandlers :
        IEventHandler<ContractCreated>,
        IEventHandler<ContractAmountUpdated>
    {
        private readonly ICrudRepository<Invoice> _invoiceRepository;

        public ContractEventHandlers(ICrudRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public Task HandleAsync(ContractCreated @event)
        {
            var invoice = new Invoice(Guid.NewGuid(), @event.ClientId, @event.Id, @event.Amount);
            return _invoiceRepository.AddAsync(invoice);
        }

        public async Task HandleAsync(ContractAmountUpdated @event)
        {
            var invoices = await _invoiceRepository.GetWhereAsync(i => i.ContractId.HasValue && i.ContractId == @event.Id);
            var invoice = invoices.FirstOrDefault();

            if (invoice != null)
            {
                invoice.UpdateAmount(@event.NewAmount);
                await _invoiceRepository.UpdateAsync(invoice);
            }
        }
    }
}
