using System;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEventSourcedRepository<TAggregateRoot> 
        where TAggregateRoot : IEventSourcedAggregateRoot, new()
    {
        Task SaveAsync(TAggregateRoot aggregate, int? expectedVersion = null);
        Task<TAggregateRoot> GetById(Guid id);
    }
}
