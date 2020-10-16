using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Common;
using MonoProject.Model.Common;
using MonoProject.Repository;
using MonoProject.Repository.Common;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public abstract class GenericService<IClass, TRepository> : IRepositoryService<IClass, int>
        where IClass : class, IPoco
        where TRepository : class, IBaseRepository<IClass>
    {
        protected readonly TRepository Repository;

        public GenericService(TRepository _repository)
        {
            Repository = _repository;
        }
        public async Task<int> GetAllCountAsync()
        {
            return await Repository.GetAllCountAsync();
        }

        public async Task<IEnumerable<IClass>> GetListAsync(PagingParameterModel pagingParameterModel, SortingParameterModel sortingParameterModel)
        {
            return await Repository.GetListAsync(pagingParameterModel, sortingParameterModel);
        }

        public async Task<IClass> GetByIDAsync(int entityKey)
        {
            return await Repository.GetByIDAsync(entityKey);
        }

        public async Task<IClass> InsertAsync(IClass entityToInsert)
        {
            entityToInsert.DateCreated = System.DateTime.Now;
            FillCurrentTime(entityToInsert);

            return await Repository.InsertAsync(entityToInsert);
        }

        public async Task<IClass> UpdateAsync(IClass entityToUpdate)
        {
            FillCurrentTime(entityToUpdate);
            return await Repository.UpdateAsync(entityToUpdate);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            return await Repository.DeleteAsync(entityKey);
        }
        public void FillCurrentTime(IClass entity)
        {
            entity.DateUpdated = System.DateTime.Now;
            entity.TimeStamp = System.DateTime.Now;
        }

    }
}
