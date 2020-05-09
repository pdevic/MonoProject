using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Autofac.Integration.Mvc;

using MonoProject.DAL;
using MonoProject.Model;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class GameInfoRepository : IGameInfoRepository //RepositoryBase<GameInfo>
    {
        /*public GameInfoRepository(GameContext context) : base(context)
        {
        }*/

        private readonly GameContext _dbContext;

        public GameInfoRepository(GameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<IGameInfo> TestList()
        {
            return _dbContext.GameInfos.ToList();
        }

        public async Task<IGameInfo> GetByIDAsync(int gameInfoID)
        {
            return await _dbContext.GameInfos.FindAsync(gameInfoID);
        }

        public async Task<IGameInfo> InsertAsync(IGameInfo entityToInsert)
        {
            entityToInsert.ID = Guid.NewGuid().GetHashCode();
            entityToInsert.DateCreated = System.DateTime.Now;
            entityToInsert.DateUpdated = System.DateTime.Now;
            entityToInsert.ReleaseDate = System.DateTime.Now; // Mock value
            entityToInsert.TimeStamp = System.DateTime.Now;

            _dbContext.GameInfos.Add((GameInfo)entityToInsert);
            await SaveChangesAsync();

            return (GameInfo)entityToInsert;
        }

        public async Task<IGameInfo> UpdateAsync(IGameInfo entityToUpdate)
        {
            _dbContext.GameInfos.Attach((GameInfo)entityToUpdate);
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
                _dbContext.Set<GameInfo>().Remove((GameInfo)entity);
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
