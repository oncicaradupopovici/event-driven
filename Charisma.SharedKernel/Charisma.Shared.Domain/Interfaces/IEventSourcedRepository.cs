using System;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEventSourcedRepository<T> where T : EventSourcedAggregateRoot, new()
    {
        Task SaveAsync(EventSourcedAggregateRoot aggregate, int? expectedVersion = null);
        Task<T> GetById(Guid id);
    }
}
