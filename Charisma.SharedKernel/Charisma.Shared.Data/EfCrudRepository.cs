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
    public class EfCrudRepository<TEntity, TContext> : ICrudRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext _c;

        public EfCrudRepository(TContext c)
        {
            _c = c;
        }


        public Task<TEntity> GetSingleAsync(Guid id, string includePath = null)
        {
            IQueryable<TEntity> dbSet = _c.Set<TEntity>();
            if(!string.IsNullOrWhiteSpace(includePath))
                dbSet = dbSet.Include(includePath);

            return dbSet.FirstOrDefaultAsync(e => e.Id == id);
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


        public Task AddAsync(TEntity entity)
        {
            _c.Set<TEntity>().Add(entity);
            return _c.SaveChangesAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            _c.Set<TEntity>().Update(entity);
            return _c.SaveChangesAsync();
        }
    }
}
