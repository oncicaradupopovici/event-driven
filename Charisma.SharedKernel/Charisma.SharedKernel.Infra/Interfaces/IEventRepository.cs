using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Core.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<EventDescriptor>> GetEventsForAggregateAsync(Guid aggregateId);
        Task AddEventAsync(EventDescriptor eventDescriptor);
    }
}
