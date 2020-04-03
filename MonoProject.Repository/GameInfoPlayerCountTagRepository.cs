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
            _dbContext.GameInfoPlayerCountTags.Add(entityToInsert);
            await SaveChangesAsync();

            return entityToInsert;
        }

        public async Task<IGameInfoPlayerCountTag> UpdateAsync(IGameInfoPlayerCountTag entityToUpdate)
        {
            _dbContext.GameInfoPlayerCountTags.Attach(entityToUpdate);
            var entry = _dbContext.Entry(entityToUpdate);

            entry.Entity.DateUpdated = System.DateTime.Now;
            await SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<IGameInfoPlayerCountTag> DeleteAsync(int entityKey)
        {
            var entity = await GetByIDAsync(entityKey);

            if (entity != null)
            {
                _dbContext.GameInfoPlayerCountTags.Remove(entity);
                await SaveChangesAsync();

                return entity;
            }

            return null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
