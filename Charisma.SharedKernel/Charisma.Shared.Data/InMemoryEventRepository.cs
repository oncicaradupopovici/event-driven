using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charisma.SharedKernel.Core;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.SharedKernel.Data
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly Dictionary<Guid, List<EventDescriptor>> _current = new Dictionary<Guid, List<EventDescriptor>>();

        public Task<IEnumerable<EventDescriptor>> GetEventsForAggregateAsync(Guid aggregateId)
        {
            if (!_current.TryGetValue(aggregateId, out List<EventDescriptor>  eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                _current.Add(aggregateId, eventDescriptors);
            }

            return Task.FromResult((IEnumerable<EventDescriptor>)eventDescriptors);
        }

        public async Task AddEventAsync(EventDescriptor eventDescriptor)
        {
            var events = await GetEventsForAggregateAsync(eventDescriptor.AggregateId) as List<EventDescriptor>;
            events?.Add(eventDescriptor);
        }
    }
}
