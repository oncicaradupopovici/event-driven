using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Data.Abstractions
{
    public interface IEventRepository
    {
        Task<IEnumerable<EventDescriptor>> GetEventsForAggregateAsync(Guid aggregateId);
        Task AddEventAsync(EventDescriptor eventDescriptor);
    }
}
