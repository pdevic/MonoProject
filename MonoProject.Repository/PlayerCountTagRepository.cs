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

        public Task<IPlayerCountTag> GetByIDAsync(int playerCountTagID)
        {
            return _dbContext.PlayerCountTags.FindAsync(playerCountTagID);
        }

        public async Task InsertAsync(IPlayerCountTag entityToInsert)
        {
            _dbContext.PlayerCountTags.Add(entityToInsert);
        }

        public async Task UpdateAsync(IPlayerCountTag entityToUpdate)
        {
            _dbContext.PlayerCountTags.Attach(entityToUpdate);
            var entry = _dbContext.Entry(entityToUpdate);

            entry.Entity.DateUpdated = System.DateTime.Now;
        }

        public async Task DeleteAsync(int entityKey)
        {
            _dbContext.PlayerCountTags.Remove(await GetByIDAsync(entityKey));
        }

        public async Task SaveChangesAsync()
        {
            _dbContext.SaveChanges();
        }
    }
}
