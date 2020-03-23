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
            return _dbContext.GameInfos.Find(gameInfoID);
        }

        public async Task InsertAsync(IGameInfo entityToInsert)
        {
            _dbContext.GameInfos.Add(entityToInsert);
        }

        public async Task UpdateAsync(IGameInfo entityToUpdate)
        {
            _dbContext.GameInfos.Attach(entityToUpdate);
            var entry = _dbContext.Entry(entityToUpdate);

            entry.Entity.DateUpdated = System.DateTime.Now;
        }

        public async Task DeleteAsync(int entityKey)
        {
            _dbContext.GameInfos.Remove(await GetByIDAsync(entityKey));
        }

        public async Task SaveChangesAsync()
        {
            _dbContext.SaveChanges();
        }
    }
}
