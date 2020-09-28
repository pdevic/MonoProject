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
                int len = name.Length;
                int lenMax = MonoProject.Common.Common.gameInfoMaxNameLength;

                if ((len > 0) && (len < lenMax))
                {
                    return true;
                }
                else
                {
                    throw new Exception(String.Format("GameInfo name length ({0}) not in range [{1}, {2}], name = \"{3}\"", len, 1, lenMax, name));
                }
            });

            await task;
            return task.Result;
        }

        public GameInfoService(IGameInfoRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<IGameInfo>> GetListAsync()
        {
            return await repository.GetListAsync();
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

            entityToInsert.ID = Guid.NewGuid().GetHashCode();
            entityToInsert.DateCreated = System.DateTime.Now;
            entityToInsert.DateUpdated = System.DateTime.Now;
            entityToInsert.ReleaseDate = System.DateTime.Now;
            entityToInsert.TimeStamp = System.DateTime.Now;

            return await repository.InsertAsync(entityToInsert);
        }

        public async Task<IGameInfo> UpdateAsync(IGameInfo entityToUpdate)
        {
            try
            {
                await ValidateGameInfoName(entityToUpdate.Name);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw new Exception("GameInfoService failed to update a GameInfo instance");
            }

            var entry = await repository.GetByIDAsync(entityToUpdate.ID);

            entry.Name = entityToUpdate.Name;
            entry.Description = entityToUpdate.Description;
            entry.ReleaseDate = entityToUpdate.ReleaseDate;

            return await repository.UpdateAsync(entry);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            return await repository.DeleteAsync(entityKey);
        }

    }
}
