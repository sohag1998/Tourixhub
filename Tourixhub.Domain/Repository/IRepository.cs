using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Domain.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity: class, IEntity<TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey Id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
    }
}
