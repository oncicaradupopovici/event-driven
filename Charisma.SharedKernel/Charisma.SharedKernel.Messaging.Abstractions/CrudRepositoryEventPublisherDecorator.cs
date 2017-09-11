//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using Charisma.SharedKernel.Core.Interfaces;
//using Charisma.SharedKernel.Domain;
//using Charisma.SharedKernel.Domain.Interfaces;
//using Charisma.SharedKernel.Core;

//namespace Charisma.SharedKernel.Messaging.Abstractions
//{
//    public class CrudRepositoryEventPublisherDecorator<TAggregateRoot> : ICrudRepository<TAggregateRoot>
//        where TAggregateRoot : class, IEventedAggregateRoot
//    {
//        private readonly ICrudRepository<TAggregateRoot> _innerRepository;
//        private readonly IEventStore _eventStore;
//        private readonly IEventBusPublisher _publisher;

//        public CrudRepositoryEventPublisherDecorator(ICrudRepository<TAggregateRoot> innerRepository, IEventStore eventStore, IEventBusPublisher publisher)
//        {
//            _innerRepository = innerRepository;
//            _eventStore = eventStore;
//            _publisher = publisher;
//        }

//        public async Task AddAsync(TAggregateRoot aggregate)
//        {
//            var events = aggregate.GetUncommittedChanges().ToList();

//            /////TODO: Transaction required here  
//            await _innerRepository.AddAsync(aggregate);
//            await PublishEventsAsync(events);
//            /////TODO: Transaction required here  
//        }

//        public async Task UpdateAsync(TAggregateRoot aggregate)
//        {
//            var events = aggregate.GetUncommittedChanges().ToList();

//            /////TODO: Transaction required here  
//            await _innerRepository.UpdateAsync(aggregate);
//            await PublishEventsAsync(events);
//            /////TODO: Transaction required here  
//        }



//        public Task<IEnumerable<TAggregateRoot>> GetAllAsync()
//        {
//            return _innerRepository.GetAllAsync();
//        }

//        public Task<IEnumerable<TAggregateRoot>> GetWhereAsync(Expression<Func<TAggregateRoot, bool>> predicate)
//        {
//            return _innerRepository.GetWhereAsync(predicate);
//        }

//        public Task<TAggregateRoot> GetSingleAsync(Guid id, string includePath = null)
//        {
//            return _innerRepository.GetSingleAsync(id, includePath);
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
