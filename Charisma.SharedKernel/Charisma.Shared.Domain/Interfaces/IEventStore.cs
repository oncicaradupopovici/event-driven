using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, IEnumerable<Event> events, int? expectedVersion = null);
        Task<List<Event>> GetEventsForAggregateAsync(Guid aggregateId);
    }
}
