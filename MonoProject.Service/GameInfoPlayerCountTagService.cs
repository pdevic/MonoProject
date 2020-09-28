using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;
using MonoProject.Repository.Common;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class GameInfoPlayerCountTagService : IGameInforPlayerCountTagService
    {
        private readonly IGameInfoPlayerCountTagRepository repository;

        public GameInfoPlayerCountTagService(IGameInfoPlayerCountTagRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<IGameInfoPlayerCountTag>> GetListAsync()
        {
            return await repository.GetListAsync();
        }

        public async Task<IGameInfoPlayerCountTag> GetByIDAsync(int entityKey)
        {
            return await repository.GetByIDAsync(entityKey);
        }

        public async Task<IGameInfoPlayerCountTag> InsertAsync(IGameInfoPlayerCountTag entityToInsert)
        {
            entityToInsert.ID = Guid.NewGuid().GetHashCode();
            entityToInsert.DateCreated = System.DateTime.Now;
            entityToInsert.DateUpdated = System.DateTime.Now;
            entityToInsert.TimeStamp = System.DateTime.Now;

            return await repository.InsertAsync(entityToInsert);
        }

        public async Task<IGameInfoPlayerCountTag> UpdateAsync(IGameInfoPlayerCountTag entityToUpdate)
        {
            var entry = await repository.GetByIDAsync(entityToUpdate.ID);

            entry.GameInfoID = entityToUpdate.GameInfoID;
            entry.PlayerCountTagID = entityToUpdate.PlayerCountTagID;

            return await repository.UpdateAsync(entry);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            return await repository.DeleteAsync(entityKey);
        }
    }
}
