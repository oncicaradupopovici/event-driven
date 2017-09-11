using Charisma.SharedKernel.EventDrivenAbstractions;
using Charisma.SharedKernel.Messaging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.Invoices.Application.DomainEventHandlers
{
    public class InvoiceDomainEventHandlers :
         IEventHandler<Domain.InvoiceAggregate.InvoiceCreated>
    {

        private readonly IEventBusPublisher _eventPublisher;

        public InvoiceDomainEventHandlers(IEventBusPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public Task HandleAsync(Domain.InvoiceAggregate.InvoiceCreated domainEvent)
        {
            return _eventPublisher.PublishAsync(
                new PublishedLanguage.Events.InvoiceCreated(domainEvent.AggregateId, domainEvent.ClientId, domainEvent.ContractId, domainEvent.Amount));
        }
    }
}
