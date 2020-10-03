using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Common;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class GameInfoPlayerCountTagService : IGameInforPlayerCountTagService
    {
        private readonly IGameInfoPlayerCountTagRepository Repository;

        public GameInfoPlayerCountTagService(IGameInfoPlayerCountTagRepository _repository)
        {
            Repository = _repository;
        }

        public async Task<int> GetAllCountAsync()
        {
            return await Repository.GetAllCountAsync();
        }
        public async Task<IEnumerable<IGameInfoPlayerCountTag>> GetListAsync(PagingParameterModel pagingParameterModel, SortingParameterModel sortingParameterModel)
        {
            return await Repository.GetListAsync(pagingParameterModel, sortingParameterModel);
        }

        public async Task<IGameInfoPlayerCountTag> GetByIDAsync(int entityKey)
        {
            return await Repository.GetByIDAsync(entityKey);
        }

        public async Task<IGameInfoPlayerCountTag> InsertAsync(IGameInfoPlayerCountTag entityToInsert)
        {
            entityToInsert.ID = Guid.NewGuid().GetHashCode();
            entityToInsert.DateCreated = System.DateTime.Now;
            entityToInsert.DateUpdated = System.DateTime.Now;
            entityToInsert.TimeStamp = System.DateTime.Now;

            return await Repository.InsertAsync(entityToInsert);
        }

        public async Task<IGameInfoPlayerCountTag> UpdateAsync(IGameInfoPlayerCountTag entityToUpdate)
        {
            var entry = await Repository.GetByIDAsync(entityToUpdate.ID);

            entry.GameInfoID = entityToUpdate.GameInfoID;
            entry.PlayerCountTagID = entityToUpdate.PlayerCountTagID;

            return await Repository.UpdateAsync(entry);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            return await Repository.DeleteAsync(entityKey);
        }

    }
}
