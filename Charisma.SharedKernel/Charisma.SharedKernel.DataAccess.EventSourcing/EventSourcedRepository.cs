using System;
using System.Linq;
using System.Threading.Tasks;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.Data.Abstractions;
using Charisma.SharedKernel.EventDrivenAbstractions;

namespace Charisma.SharedKernel.Data.EventSourcing
{
    public class EventSourcedRepository<TAggregateRoot> : IEventSourcedRepository<TAggregateRoot> 
        where TAggregateRoot : IEventSourcedAggregateRoot, new() //shortcut you can do as you see fit with new()
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _eventPublisher;

        public EventSourcedRepository(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
            
        }

        public async Task SaveAsync(TAggregateRoot aggregate, int? expectedVersion = null)
        {
            var events = aggregate.GetUncommittedChanges().ToList();
            await _eventStore.SaveEventsForAggregateAsync(aggregate.Id, events, expectedVersion);
            
            var tasks = events.Select(async e => await _eventPublisher.PublishAsync(e));
            await Task.WhenAll(tasks);

            aggregate.MarkChangesAsCommitted();
        }

        public async Task<TAggregateRoot> GetById(Guid id)
        {
            var obj = new TAggregateRoot();//lots of ways to do this
            var e = await _eventStore.GetEventsForAggregateAsync(id);
            obj.LoadFromHistory(e);
            return obj;
        }
    }
}
