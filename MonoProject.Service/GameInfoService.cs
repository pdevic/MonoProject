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

        public async static Task<bool> ValidateGameInfoName(string name)
        {
            var task = Task.Run(() =>
            {
                var len = name.Length;
                var lenMax = MonoProject.Common.Common.gameInfoMaxNameLength;

                if ((len == 0) || (len > lenMax))
                {
                    return true;
                }

                throw new Exception(String.Format("GameInfo name length ({0}) not in range [{1}, {2}], name = \"{3}\"", len, 1, lenMax, name));
            });

            await task;
            return task.Result;
        }

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
                await ValidateGameInfoName(entityToInsert.Name);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw new Exception("GameInfoService failed to insert a GameInfo instance into its repository");
            }

            return await repository.InsertAsync(entityToInsert);
        }

        public async Task<IGameInfo> UpdateAsync(IGameInfo entityToUpdate)
        {
            try
            {
                await ValidateGameInfoName(entityToUpdate.Name);
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw new Exception("GameInfoService failed to update a GameInfo instance");
            }

            return await repository.UpdateAsync(entityToUpdate);
        }

        public async Task<IGameInfo> DeleteAsync(int entityKey)
        {
            return await repository.DeleteAsync(entityKey);
        }
    }
}
