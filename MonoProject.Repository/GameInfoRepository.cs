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
            _dbContext.GameInfos.Add(entityToInsert);
            await SaveChangesAsync();

            return entityToInsert;
        }

        public async Task<IGameInfo> UpdateAsync(IGameInfo entityToUpdate)
        {
            _dbContext.GameInfos.Attach(entityToUpdate);
            var entry = _dbContext.Entry(entityToUpdate);

            entry.Entity.DateUpdated = System.DateTime.Now;
            await SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<IGameInfo> DeleteAsync(int entityKey)
        {
            var entity = await GetByIDAsync(entityKey);

            if (entity != null)
            {
                _dbContext.GameInfos.Remove(entity);
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
