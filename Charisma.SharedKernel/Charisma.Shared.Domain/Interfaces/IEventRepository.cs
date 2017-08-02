using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<EventDescriptor>> GetEventsForAggregateAsync(Guid aggregateId);
        Task AddEventAsync(EventDescriptor eventDescriptor);
    }
}
