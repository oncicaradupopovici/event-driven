//using Charisma.SharedKernel.Core;
//using Charisma.SharedKernel.Domain.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Charisma.SharedKernel.Messaging.Abstractions
//{
//    public class EventSourcedRepositoryEventPublisherDecorator<TAggregateRoot> : IEventSourcedRepository<TAggregateRoot>
//         where TAggregateRoot : IEventSourcedAggregateRoot, new()
//    {
//        private readonly IEventBusPublisher _publisher;
//        private readonly IEventSourcedRepository<TAggregateRoot> _innerRepository;


//        public EventSourcedRepositoryEventPublisherDecorator(IEventSourcedRepository<TAggregateRoot> innerRepository, IEventBusPublisher publisher)
//        {
//            _innerRepository = innerRepository;
//            _publisher = publisher;
//        }

//        public Task<TAggregateRoot> GetById(Guid id)
//        {
//            return _innerRepository.GetById(id);
//        }

//        public async Task SaveAsync(TAggregateRoot aggregate, int? expectedVersion = null)
//        {
//            var events = aggregate.GetUncommittedChanges().ToList();

//            /////TODO: Transaction required here  
//            await _innerRepository.SaveAsync(aggregate, expectedVersion);
//            await PublishEventsAsync(events);
//            /////TODO: Transaction required here  
//        }

//        private async Task PublishEventsAsync(List<Event> events)
//        {
//            foreach (var @event in events)
//            {
//                await _publisher.PublishAsync(@event);
//            }
//        }
//    }
//}
