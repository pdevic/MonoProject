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
    public class PlayerCountTagService : IPlayerCountTagService
    {
        private readonly IPlayerCountTagRepository repository;

        public PlayerCountTagService(IPlayerCountTagRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IPlayerCountTag> GetByIDAsync(int entityKey)
        {
            return await repository.GetByIDAsync(entityKey);
        }

        public async Task<IPlayerCountTag> InsertAsync(IPlayerCountTag entityToInsert)
        {
            try
            {
                await GameService.ValidatePlayerCountTagName(entityToInsert.Name);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw new Exception("GameInfoService failed to insert a GameInfo instance into its repository");
            }

            return await repository.InsertAsync(entityToInsert);
        }

        public async Task<IPlayerCountTag> UpdateAsync(IPlayerCountTag entityToUpdate)
        {
            try
            {
                await GameService.ValidatePlayerCountTagName(entityToUpdate.Name);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw new Exception("PlayerCountTagService failed to update a PlayerCountTag instance");
            }

            return await repository.UpdateAsync(entityToUpdate);
        }

        public async Task<IPlayerCountTag> DeleteAsync(int entityKey)
        {
            return await repository.DeleteAsync(entityKey);
        }
    }
}
