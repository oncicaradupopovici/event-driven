using System;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEventSourcedRepository<T> where T : AggregateRoot, new()
    {
        Task SaveAsync(AggregateRoot aggregate, int? expectedVersion = null);
        Task<T> GetById(Guid id);
    }
}
