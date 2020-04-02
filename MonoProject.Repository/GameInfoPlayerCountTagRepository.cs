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
    public class GameInfoPlayerCountTagRepository : IGameInfoPlayerCountTagRepository
    {
        private readonly GameContext _dbContext;

        public GameInfoPlayerCountTagRepository(GameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IGameInfoPlayerCountTag> GetByIDAsync(int gameInfoPlayerCountTagID)
        {
            return await _dbContext.GameInfoPlayerCountTags.FindAsync(gameInfoPlayerCountTagID);
        }

        public async Task<IGameInfoPlayerCountTag> InsertAsync(IGameInfoPlayerCountTag entityToInsert)
        {
            var task = Task.Run(async () => { 
                var res = _dbContext.GameInfoPlayerCountTags.Add(entityToInsert);
                await SaveChangesAsync();

                return res;
            });

            return await task;
        }

        public async Task<IGameInfoPlayerCountTag> UpdateAsync(IGameInfoPlayerCountTag entityToUpdate)
        {
            var task = Task.Run(async () =>
            {
                _dbContext.GameInfoPlayerCountTags.Attach(entityToUpdate);
                var entry = _dbContext.Entry(entityToUpdate);

                entry.Entity.DateUpdated = System.DateTime.Now;
                await SaveChangesAsync();

                return entry;
            });

            await task;
            return task.Result.Entity;
        }

        public async Task<IGameInfoPlayerCountTag> DeleteAsync(int entityKey)
        {
            if (GetByIDAsync(entityKey) != null)
            {
                var task = Task.Run(async () => {
                    var res = _dbContext.GameInfoPlayerCountTags.Remove(await GetByIDAsync(entityKey));
                    await SaveChangesAsync();

                    return res;
                });
                return (await task);
            }

            return null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
