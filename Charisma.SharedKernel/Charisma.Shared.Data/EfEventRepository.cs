using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Charisma.SharedKernel.Data
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
            var dbSet = _c.Set<EventDescriptor>();
            return await dbSet.ToListAsync();
        }

        public async Task AddEventAsync(EventDescriptor eventDescriptor)
        {
            _c.Set<EventDescriptor>().Add(eventDescriptor);
            await _c.SaveChangesAsync();
        }
    }
}
