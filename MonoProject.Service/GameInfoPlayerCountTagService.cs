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

        public async Task<IGameInfoPlayerCountTag> GetByIDAsync(int entityKey)
        {
            return await repository.GetByIDAsync(entityKey);
        }

        public async Task<IGameInfoPlayerCountTag> InsertAsync(IGameInfoPlayerCountTag entityToInsert)
        {
            return await repository.InsertAsync(entityToInsert);
        }

        public async Task<IGameInfoPlayerCountTag> UpdateAsync(IGameInfoPlayerCountTag entityToUpdate)
        {
            return await repository.UpdateAsync(entityToUpdate);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            return await repository.DeleteAsync(entityKey);
        }
    }
}
