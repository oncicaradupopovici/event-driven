using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Charisma.Invoices.PublishedLanguage;
using Charisma.Invoices.PublishedLanguage.Events;
using Charisma.Payments.Domain.PayableAggregate;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Payments.Application.EventHandlers
{
    public class InvoiceEventHandlers : IEventHandler<InvoiceCreated>
    {
        private readonly ICrudRepository<Payable> _repository;

        public InvoiceEventHandlers(ICrudRepository<Payable> repository)
        {
            this._repository = repository;
        }

        public async Task HandleAsync(InvoiceCreated @event)
        {
            var payable = new Payable(@event.ClientId, @event.Amount, @event.AggregateId);
            await _repository.AddAsync(payable);
        }
    }
}
