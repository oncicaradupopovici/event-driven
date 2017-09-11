using Charisma.SharedKernel.Data.Abstractions;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.EventDrivenAbstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Data.EntityFramework
{
    public class EfEventedCrudRepository<TEntity, TContext> : EfCrudRepository<TEntity, TContext>
        where TEntity : class, IEventedAggregateRoot
        where TContext : DbContext
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _eventPublisher;

        public EfEventedCrudRepository(TContext c, IEventStore eventStore, IEventPublisher eventPublisher)
            :base(c)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }

        public override async Task SaveChangesAsync()
        {
            var domainEntities = _c.ChangeTracker
                .Entries<IEventedAggregateRoot>();


            var saveInEventStoreTasks = domainEntities
                .Select(async e => await _eventStore.SaveEventsForAggregateAsync(e.Entity.Id, e.Entity.GetUncommittedChanges()));

            await Task.WhenAll(saveInEventStoreTasks);

            var publishTasks = domainEntities
                .SelectMany(x => x.Entity.GetUncommittedChanges())
                .Select(async e => await _eventPublisher.PublishAsync(e));

            await Task.WhenAll(publishTasks);

            foreach(var de in domainEntities)
            {
                de.Entity.MarkChangesAsCommitted();
            }


            await base.SaveChangesAsync();
        }

    }
}
