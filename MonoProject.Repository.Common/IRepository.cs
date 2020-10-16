using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Common;

namespace MonoProject.Repository.Common
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        //IEnumerable<TEntity> Get();
        //Task<IEnumerable<TEntity>> GetAsync();

        //TEntity GetByID(TKey entityKey);
        Task<TEntity> GetByIDAsync(TKey entityKey);
        Task<int> GetAllCountAsync();
        Task<IEnumerable<TEntity>> GetListAsync(PagingParameterModel pagingParameterModel, SortingParameterModel sortingParameterModel, SearchParameters searchParameters);

        Task<TEntity> InsertAsync(TEntity entityToInsert);
        Task<TEntity> UpdateAsync(TEntity entityToUpdate);
        Task<bool> DeleteAsync(TKey entityKey);

        Task<int> SaveChangesAsync();
    }
}
