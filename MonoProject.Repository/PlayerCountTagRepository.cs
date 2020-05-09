using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.DAL;
using MonoProject.Model;
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
            _dbContext.Set<PlayerCountTag>().Add((PlayerCountTag)entityToInsert);
            await SaveChangesAsync();

            return entityToInsert;
        }

        public async Task<IPlayerCountTag> UpdateAsync(IPlayerCountTag entityToUpdate)
        {
            _dbContext.Set<PlayerCountTag>().Attach((PlayerCountTag)entityToUpdate);
            var entry = _dbContext.Entry(entityToUpdate);

            entry.Entity.DateUpdated = System.DateTime.Now;
            await SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            var entity = await GetByIDAsync(entityKey);

            if (entity != null)
            {
                _dbContext.Set<PlayerCountTag>().Remove((PlayerCountTag)entity);
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
