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
    public class PlayerCountTagRepository : IPlayerCountTagRepository
    {
        private readonly GameContext _dbContext;

        public PlayerCountTagRepository(GameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IPlayerCountTag> GetByIDAsync(int playerCountTagID)
        {
            return (await _dbContext.PlayerCountTags.FindAsync(playerCountTagID));
        }

        public async Task<IPlayerCountTag> InsertAsync(IPlayerCountTag entityToInsert)
        {
            var task = Task.Run(() => _dbContext.PlayerCountTags.Add(entityToInsert));
            return await task;
        }

        public async Task<IPlayerCountTag> UpdateAsync(IPlayerCountTag entityToUpdate)
        {
            var task = Task.Run(() =>
            {
                _dbContext.PlayerCountTags.Attach(entityToUpdate);
                var entry = _dbContext.Entry(entityToUpdate);

                entry.Entity.DateUpdated = System.DateTime.Now;
                return entry;
            });

            await task;
            return task.Result.Entity;
        }

        public async Task<IPlayerCountTag> DeleteAsync(int entityKey)
        {
            if (GetByIDAsync(entityKey) != null)
            {
                var task = Task.Run(async () => _dbContext.PlayerCountTags.Remove(await GetByIDAsync(entityKey)));
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
