using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Charisma.SharedKernel.Data
{
    public class EfCrudRepository<TAggregateRoot, TContext> : ICrudRepository<TAggregateRoot>
        where TAggregateRoot : EventedAggregateRoot
        where TContext : DbContext
    {
        private readonly TContext _c;
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _publisher;

        public EfCrudRepository(TContext c, IEventStore eventStore, IEventPublisher publisher)
        {
            _c = c;
            _eventStore = eventStore;
            _publisher = publisher;
        }

        public async Task AddAsync(TAggregateRoot entity)
        {
            _c.Set<TAggregateRoot>().Add(entity);
            
            await SaveAndPublishEventsAsync(entity);
        }

        public async Task UpdateAsync(TAggregateRoot entity)
        {
            _c.Set<TAggregateRoot>().Update(entity);
            await SaveAndPublishEventsAsync(entity);
        }



        public async Task<IEnumerable<TAggregateRoot>> GetAllAsync()
        {
            var list = await _c.Set<TAggregateRoot>().ToListAsync();
            return list;
        }

        public async Task<IEnumerable<TAggregateRoot>> GetWhereAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            var result = await _c.Set<TAggregateRoot>().Where(predicate).ToListAsync();
            return result;
        }

        public Task<TAggregateRoot> GetSingleAsync(Guid id)
        {
            return _c.Set<TAggregateRoot>().FirstOrDefaultAsync(e => e.Id == id);
        }

        private async Task SaveAndPublishEventsAsync(EventedAggregateRoot aggregate)
        {
            var events = aggregate.GetUncommittedChanges().ToList();

            /////TODO: Transaction required here
            await _c.SaveChangesAsync();
            
            await _eventStore.SaveEventsAsync(aggregate.Id, events);

            foreach (var @event in events)
            {
                await _publisher.PublishAsync(@event);
            }

            aggregate.MarkChangesAsCommitted();
            /////TODO: Transaction required here
        }
    }
}
