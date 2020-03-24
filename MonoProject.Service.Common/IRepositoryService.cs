using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Repository.Common;

namespace MonoProject.Service.Common
{
    public interface IRepositoryService<TEntity, in TKey> where TEntity :class
    {
        Task<TEntity> GetByIDAsync(TKey entityKey);

        Task<TEntity> InsertAsync(TEntity entityToInsert);
        Task<TEntity> UpdateAsync(TEntity entityToUpdate);
        Task<TEntity> DeleteAsync(TKey entityKey);
    }
}
