using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.DAL;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class GameInfoRepository : IGameInfoRepository
    {
        private readonly GameContext _dbContext;

        public GameInfoRepository(GameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IGameInfo> GetByIDAsync(int gameInfoID)
        {
            return await _dbContext.GameInfos.FindAsync(gameInfoID);
        }

        public async Task<IGameInfo> InsertAsync(IGameInfo entityToInsert)
        {
            var task = Task.Run(() => _dbContext.GameInfos.Add(entityToInsert));
            return await task;
        }

        public async Task<IGameInfo> UpdateAsync(IGameInfo entityToUpdate)
        {
            var task = Task.Run(() =>
            {
                _dbContext.GameInfos.Attach(entityToUpdate);
                var entry = _dbContext.Entry(entityToUpdate);

                entry.Entity.DateUpdated = System.DateTime.Now;
                return entry;
            });

            await task;
            return task.Result.Entity;
        }

        public async Task<IGameInfo> DeleteAsync(int entityKey)
        {
            if (GetByIDAsync(entityKey) != null)
            {
                var task = Task.Run(async () => _dbContext.GameInfos.Remove(await GetByIDAsync(entityKey)));
                return (await task);
            }

            return null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync());
        }
    }
}
