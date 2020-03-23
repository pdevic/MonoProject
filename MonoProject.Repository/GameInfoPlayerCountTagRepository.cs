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

        public Task<IGameInfoPlayerCountTag> GetByIDAsync(int gameInfoPlayerCountTagID)
        {
            return _dbContext.GameInfoPlayerCountTags.FindAsync(gameInfoPlayerCountTagID);
        }

        public async Task InsertAsync(IGameInfoPlayerCountTag entityToInsert)
        {
            _dbContext.GameInfoPlayerCountTags.Add(entityToInsert);
        }

        public async Task UpdateAsync(IGameInfoPlayerCountTag entityToUpdate)
        {
            _dbContext.GameInfoPlayerCountTags.Attach(entityToUpdate);
            var entry = _dbContext.Entry(entityToUpdate);

            entry.Entity.DateUpdated = System.DateTime.Now;
        }

        public async Task DeleteAsync(int entityKey)
        {
            _dbContext.GameInfoPlayerCountTags.Remove(await GetByIDAsync(entityKey));
        }

        public async Task SaveChangesAsync()
        {
            _dbContext.SaveChanges();
        }
    }
}
