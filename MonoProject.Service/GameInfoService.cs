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
    public class GameInfoService : IGameInfoService
    {
        private readonly IGameInfoRepository repository;

        public GameInfoService(IGameInfoRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IGameInfo> GetByIDAsync(int entityKey)
        {
            return await repository.GetByIDAsync(entityKey);
        }

        public async Task<IGameInfo> InsertAsync(IGameInfo entityToInsert)
        {
            try
            {
                await GameService.ValidateGameInfoName(entityToInsert.Name);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw new Exception("GameInfoService failed to insert a GameInfo into its repository");
            }

            return await repository.InsertAsync(entityToInsert);
        }

        public async Task<IGameInfo> UpdateAsync(IGameInfo entityToUpdate)
        {
            try
            {
                await GameService.ValidateGameInfoName(entityToUpdate.Name);
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw new Exception("GameInfoService failed to update a GameInfo");
            }

            return await repository.UpdateAsync(entityToUpdate);
        }

        public async Task<IGameInfo> DeleteAsync(int entityKey)
        {
            return await repository.DeleteAsync(entityKey);
        }
    }
}
