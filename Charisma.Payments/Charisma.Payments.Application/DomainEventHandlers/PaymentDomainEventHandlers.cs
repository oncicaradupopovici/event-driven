using Charisma.SharedKernel.EventDrivenAbstractions;
using Charisma.SharedKernel.Messaging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.Payments.Application.DomainEventHandlers
{
    public class PaymentDomainEventHandlers :
        IEventHandler<Domain.PayableAggregate.PaymentReceived>
    {
        private readonly IEventBusPublisher _eventPublisher;

        public PaymentDomainEventHandlers(IEventBusPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public Task HandleAsync(Domain.PayableAggregate.PaymentReceived domainEvent)
        {
            return _eventPublisher.PublishAsync(
                new PublishedLanguage.Events.PaymentReceived(domainEvent.AggregateId, domainEvent.PaymentId, domainEvent.InvoiceId, domainEvent.PaymentDate));
        }
    }
}
