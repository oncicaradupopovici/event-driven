using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.ReadModel;
using Charisma.SharedKernel.ReadModel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Charisma.SharedKernel.Data
{
    public class EfReadModelRepository<TEntity, TContext> : IReadModelRepository<TEntity>
        where TEntity : ReadModelEntity
        where TContext : DbContext
    {
        private readonly TContext _c;

        public EfReadModelRepository(TContext c)
        {
            _c = c;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var list = await _c.Set<TEntity>().ToListAsync();
            return list;
        }

        public Task<TEntity> GetSingleAsync(Guid id)
        {
            return _c.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task AddAsync(TEntity entity)
        {
            _c.Set<TEntity>().Add(entity);
            return _c.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _c.Set<TEntity>().Update(entity);
            await _c.SaveChangesAsync();
        }

        

    }
}
