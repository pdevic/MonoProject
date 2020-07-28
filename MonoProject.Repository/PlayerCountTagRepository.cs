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
    public class PlayerCountTagRepository : IPlayerCountTagRepository
    {
        private readonly GameContext _dbContext;

        public PlayerCountTagRepository(GameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IPlayerCountTag> GetByIDAsync(int playerCountTagID)
        {
            return await _dbContext.PlayerCountTags.FindAsync(playerCountTagID);
        }

        public async Task<IEnumerable<IPlayerCountTag>> ListAsync()
        {
            return await _dbContext.PlayerCountTags.ToListAsync();
        }

        public async Task<IPlayerCountTag> InsertAsync(IPlayerCountTag entityToInsert)
        {
            entityToInsert.ID = Guid.NewGuid().GetHashCode();
            entityToInsert.DateCreated = System.DateTime.Now;
            entityToInsert.DateUpdated = System.DateTime.Now;
            entityToInsert.TimeStamp = System.DateTime.Now;

            _dbContext.PlayerCountTags.Add((PlayerCountTag)entityToInsert);
            await SaveChangesAsync();

            return (PlayerCountTag)entityToInsert;
        }

        public async Task<IPlayerCountTag> UpdateAsync(IPlayerCountTag entityToUpdate)
        {
            var original = await _dbContext.PlayerCountTags.FindAsync(entityToUpdate.ID);
            var entry = _dbContext.Entry(original);

            entry.Entity.Name = entityToUpdate.Name;

            entry.Entity.DateUpdated = System.DateTime.Now;
            entry.State = EntityState.Modified;

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
