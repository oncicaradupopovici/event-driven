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
    public class EfCrudRepositoryEventedDecorator<TAggregateRoot> : ICrudRepository<TAggregateRoot>
        where TAggregateRoot : EventedAggregateRoot
    {
        private readonly ICrudRepository<TAggregateRoot> _innerRepository;
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _publisher;

        public EfCrudRepositoryEventedDecorator(ICrudRepository<TAggregateRoot> innerRepository, IEventStore eventStore, IEventPublisher publisher)
        {
            _innerRepository = innerRepository;
            _eventStore = eventStore;
            _publisher = publisher;
        }

        public async Task AddAsync(TAggregateRoot entity)
        {
            await _innerRepository.AddAsync(entity);
            await SaveAndPublishEventsAsync(entity);
        }

        public async Task UpdateAsync(TAggregateRoot entity)
        {
            await _innerRepository.UpdateAsync(entity);
            await SaveAndPublishEventsAsync(entity);
        }



        public Task<IEnumerable<TAggregateRoot>> GetAllAsync()
        {
            return _innerRepository.GetAllAsync();
        }

        public Task<IEnumerable<TAggregateRoot>> GetWhereAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return _innerRepository.GetWhereAsync(predicate);
        }

        public Task<TAggregateRoot> GetSingleAsync(Guid id, string includePath = null)
        {
            return _innerRepository.GetSingleAsync(id, includePath);
        }

        private async Task SaveAndPublishEventsAsync(EventedAggregateRoot aggregate)
        {
            var events = aggregate.GetUncommittedChanges().ToList();

            /////TODO: Transaction required here         
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
