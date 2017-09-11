using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface ICrudRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity> GetSingleAsync(Guid id, string includePath = null);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task SaveChangesAsync();

    }
}
