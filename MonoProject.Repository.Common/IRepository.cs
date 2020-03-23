using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoProject.Repository.Common
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        Task<IEnumerable<TEntity>> GetAsync();

        TEntity GetByID(TKey entityKey);
        Task<TEntity> GetByIDAsync(TKey entityKey);

        void Insert(TEntity entityToInsert);
        void Update(TEntity entityToUpdate);

        //void Delete(TEntity entityToDelete);
        void Delete(TKey entityKey);

        void SaveChanges();
    }
}
