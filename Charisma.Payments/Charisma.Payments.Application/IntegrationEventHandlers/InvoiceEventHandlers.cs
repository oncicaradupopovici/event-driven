using System.Threading.Tasks;
using Charisma.Invoices.PublishedLanguage.Events;
using Charisma.Payments.Domain.PayableAggregate;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.EventDrivenAbstractions;

namespace Charisma.Payments.Application.IntegrationEventHandlers
{
    public class InvoiceIntegrationEventHandlers : IEventHandler<InvoiceCreated>
    {
        private readonly ICrudRepository<Payable> _repository;

        public InvoiceIntegrationEventHandlers(ICrudRepository<Payable> repository)
        {
            this._repository = repository;
        }

        public async Task HandleAsync(InvoiceCreated @event)
        {
            var payable = new Payable(@event.ClientId, @event.Amount, @event.InvoiceId);
            await _repository.AddAsync(payable);
            await _repository.SaveChangesAsync();
        }
    }
}
