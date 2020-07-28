using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<IEnumerable<IGameInfo>> ListAsync()
        {
            return await _dbContext.GameInfos.ToListAsync();
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
            var original = await _dbContext.GameInfos.FindAsync(entityToUpdate.ID);
            var entry = _dbContext.Entry(original);

            entry.Entity.Name = entityToUpdate.Name;
            entry.Entity.Description = entityToUpdate.Description;
            entry.Entity.ReleaseDate = entityToUpdate.ReleaseDate;
            //entry.Entity.PlayerCountTags = entityToUpdate.PlayerCountTags;

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
