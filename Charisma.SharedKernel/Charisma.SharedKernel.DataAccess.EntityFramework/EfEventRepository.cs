using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Charisma.SharedKernel.Data.Abstractions;

namespace Charisma.SharedKernel.Data.EntityFramework
{
    public class EfEventRepository<TContext> : IEventRepository
        where TContext : DbContext
    {
        private readonly TContext _c;

        public EfEventRepository(TContext c)
        {
            _c = c;
        }

        public async Task<IEnumerable<EventDescriptor>> GetEventsForAggregateAsync(Guid aggregateId)
        {
            var dbSet = _c.Set<EventDescriptor>().Where(ed => ed.AggregateId == aggregateId);
            return await dbSet.ToListAsync();
        }

        public async Task AddEventAsync(EventDescriptor eventDescriptor)
        {
            _c.Set<EventDescriptor>().Add(eventDescriptor);
            await _c.SaveChangesAsync();
        }
    }
}
