using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IReadModelRepository<TEntity>
        where TEntity : ReadModelEntity
    {
        Task<TEntity> GetSingleAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        Task SaveAsync();
    }
}
