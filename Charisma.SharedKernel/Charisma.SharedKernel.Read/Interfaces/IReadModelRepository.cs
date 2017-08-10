﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.ReadModel.Interfaces
{
    public interface IReadModelRepository<TEntity>
        where TEntity : ReadModelEntity
    {
        Task<TEntity> GetSingleAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
    }
}