using Charisma.SharedKernel.EventDrivenAbstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Charisma.SharedKernel.Messaging.Abstractions;

namespace Charisma.Contracts.Application.DomainEventHandlers
{
    public class ContractDomainEventHandlers:
        IEventHandler<Domain.ContractAggregate.ContractCreated>,
        IEventHandler<Domain.ContractAggregate.ContractAmountUpdated>,
        IEventHandler<Domain.ContractAggregate.ContractLineAdded>,
        IEventHandler<Domain.ContractAggregate.ContractValidated>
    {
        private readonly IEventBusPublisher _eventPublisher;

        public ContractDomainEventHandlers(IEventBusPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        

        public Task HandleAsync(Domain.ContractAggregate.ContractCreated domainEvent)
        {
            return _eventPublisher.PublishAsync(
                new PublishedLanguage.Events.ContractCreated(domainEvent.AggregateId, domainEvent.ClientId, domainEvent.Version));
        }

        public Task HandleAsync(Domain.ContractAggregate.ContractAmountUpdated domainEvent)
        {
            return _eventPublisher.PublishAsync(
                new PublishedLanguage.Events.ContractAmountUpdated(domainEvent.AggregateId, domainEvent.NewAmount, domainEvent.Version));
        }

        public Task HandleAsync(Domain.ContractAggregate.ContractLineAdded domainEvent)
        {
            return _eventPublisher.PublishAsync(
                new PublishedLanguage.Events.ContractLineAdded(domainEvent.AggregateId, domainEvent.ContractLineId, domainEvent.Product, domainEvent.Price, domainEvent.Quantity, domainEvent.Version));
        }

        public Task HandleAsync(Domain.ContractAggregate.ContractValidated domainEvent)
        {
            return _eventPublisher.PublishAsync(
                new PublishedLanguage.Events.ContractValidated(domainEvent.AggregateId, domainEvent.ClientId, domainEvent.Amount));
        }
    }
}
