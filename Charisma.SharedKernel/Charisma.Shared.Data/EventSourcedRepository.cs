using System;
using System.Linq;
using System.Threading.Tasks;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.SharedKernel.Data
{
    public class EventSourcedRepository<T> : IEventSourcedRepository<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _publisher;

        public EventSourcedRepository(IEventStore eventStore, IEventPublisher publisher)
        {
            _eventStore = eventStore;
            _publisher = publisher;
        }

        public async Task SaveAsync(AggregateRoot aggregate, int? expectedVersion = null)
        {
            var events = aggregate.GetUncommittedChanges().ToList();

            /////TODO: Transaction required here
            await _eventStore.SaveEventsAsync(aggregate.Id, events, expectedVersion);

            foreach (var @event in events)
            {
                await _publisher.PublishAsync(@event);
            }

            aggregate.MarkChangesAsCommitted();
            /////TODO: Transaction required here
        }

        public async Task<T> GetById(Guid id)
        {
            var obj = new T();//lots of ways to do this
            var e = await _eventStore.GetEventsForAggregateAsync(id);
            obj.LoadsFromHistory(e);
            return obj;
        }
    }
}
