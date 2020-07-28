using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.DAL;
using MonoProject.Model;
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

        public async Task<IEnumerable<IGameInfoPlayerCountTag>> ListAsync()
        {
            return await _dbContext.GameInfoPlayerCountTags.ToListAsync();
        }

        public async Task<IGameInfoPlayerCountTag> InsertAsync(IGameInfoPlayerCountTag entityToInsert)
        {
            entityToInsert.ID = Guid.NewGuid().GetHashCode();
            entityToInsert.DateCreated = System.DateTime.Now;
            entityToInsert.DateUpdated = System.DateTime.Now;
            entityToInsert.TimeStamp = System.DateTime.Now;

            _dbContext.Set<GameInfoPlayerCountTag>().Add((GameInfoPlayerCountTag)entityToInsert);
            await SaveChangesAsync();

            return entityToInsert;
        }

        public async Task<IGameInfoPlayerCountTag> UpdateAsync(IGameInfoPlayerCountTag entityToUpdate)
        {
            var original = await _dbContext.GameInfoPlayerCountTags.FindAsync(entityToUpdate.ID);
            var entry = _dbContext.Entry(original);

            entry.Entity.GameInfoID = entityToUpdate.GameInfoID;
            entry.Entity.PlayerCountTagID = entityToUpdate.PlayerCountTagID;

            await SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            var entity = await GetByIDAsync(entityKey);

            if (entity != null)
            {
                _dbContext.Set<GameInfoPlayerCountTag>().Remove((GameInfoPlayerCountTag)entity);
                await SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
