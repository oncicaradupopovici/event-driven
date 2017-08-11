using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Charisma.Invoices.PublishedLanguage;
using Charisma.Payments.Domain.PaymentAggregate;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Payments.Application.EventHandlers
{
    public class InvoiceEventHandlers : IEventHandler<InvoiceCreated>
    {
        private readonly IEventSourcedRepository<Payment> _repository;

        public InvoiceEventHandlers(IEventSourcedRepository<Payment> repository)
        {
            this._repository = repository;
        }

        public async Task HandleAsync(InvoiceCreated @event)
        {
            var payment = new Payment(@event.ClientId, @event.Amount, @event.AggregateId);
            await _repository.SaveAsync(payment);
        }
    }
}
