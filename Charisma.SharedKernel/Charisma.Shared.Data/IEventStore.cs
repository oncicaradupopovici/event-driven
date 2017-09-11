using Charisma.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Data.Abstractions
{
    public interface IEventStore
    {
        Task SaveEventsForAggregateAsync(Guid aggregateId, IEnumerable<DomainEvent> events, int? expectedVersion = null);
        Task<List<DomainEvent>> GetEventsForAggregateAsync(Guid aggregateId);
    }
}
