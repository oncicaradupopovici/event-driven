using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Charisma.SharedKernel.Data
{
    public class EfCrudRepository<TEntity, TContext> : ICrudRepository<TEntity>
        where TEntity : AggregateRoot
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

        public async Task AddAsync(TEntity entity)
        {
            _c.Set<TEntity>().Add(entity);
            
            await SaveAndPublishEventsAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _c.Set<TEntity>().Update(entity);
            await SaveAndPublishEventsAsync(entity);
        }



        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var list = await _c.Set<TEntity>().ToListAsync();
            return list;
        }

        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _c.Set<TEntity>().Where(predicate).ToListAsync();
            return result;
        }

        public Task<TEntity> GetSingleAsync(Guid id)
        {
            return _c.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        private async Task SaveAndPublishEventsAsync(AggregateRoot aggregate)
        {
            var events = aggregate.GetUncommittedChanges().ToList();

            /////TODO: Transaction required here
            await _c.SaveChangesAsync();
            
            await _eventStore.SaveEventsAsync(aggregate.Id, events);

            foreach (var @event in events)
            {
                await _publisher.PublishAsync(@event);
            }
            /////TODO: Transaction required here
        }
    }
}
