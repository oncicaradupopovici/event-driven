using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charisma.SharedKernel.Domain;

namespace Charisma.SharedKernel.Data.Abstractions
{

    public class EventStore : IEventStore
    {
        private readonly IEventRepository _eventRepository;

        public EventStore(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }


        public async Task SaveEventsForAggregateAsync(Guid aggregateId, IEnumerable<DomainEvent> events, int? expectedVersion = null)
        {
            var eventDescriptors = await _eventRepository.GetEventsForAggregateAsync(aggregateId);

            // check whether latest event version matches current aggregate version
            // otherwise -> throw exception
            var lastEvent = eventDescriptors.LastOrDefault();
            if (expectedVersion.HasValue && lastEvent != null && lastEvent.Version != expectedVersion)
            {
                throw new ConcurrencyException();
            }
            var i = lastEvent?.Version ?? -1;

            // iterate through current aggregate events increasing version with each processed event
            foreach (var @event in events)
            {
                i++;
                @event.Version = i;

                // push event to the event descriptors list for current aggregate
                await _eventRepository.AddEventAsync(new EventDescriptor(aggregateId, @event, i, @event.CreationDate));
            }
        }

        // collect all processed events for given aggregate and return them as a list
        // used to build up an aggregate from its history (Domain.LoadsFromHistory)
        public async Task<List<DomainEvent>> GetEventsForAggregateAsync(Guid aggregateId)
        {
            var eventDescriptors = await _eventRepository.GetEventsForAggregateAsync(aggregateId);
            var evDescriptors = eventDescriptors.ToList();

            if (!evDescriptors.Any())
            {
                throw new AggregateNotFoundException();
            }

            return evDescriptors.Select(desc => desc.EventData).ToList();
        }
    }

    public class AggregateNotFoundException : Exception
    {
    }

    public class ConcurrencyException : Exception
    {
    }
}
