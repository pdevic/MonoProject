using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Autofac.Integration.Mvc;

using AutoMapper;

using MonoProject.DAL;
using MonoProject.Model;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class GameInfoRepository : IGameInfoRepository
    {
        private readonly GameContext DbContext;
        private readonly IMapper Mapper;

        public GameInfoRepository(GameContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public async Task<IEnumerable<IGameInfo>> GetListAsync()
        {
            return Mapper.Map<List<GameInfo>>(await DbContext.GameInfoEntities.ToListAsync());
        }

        public async Task<IGameInfo> GetByIDAsync(int gameInfoID)
        {
            return Mapper.Map<GameInfo>(await DbContext.GameInfoEntities.AsNoTracking().SingleOrDefaultAsync(x => x.ID == gameInfoID));
        }

        public async Task<IGameInfo> InsertAsync(IGameInfo entityToInsert)
        {
            var entry = DbContext.GameInfoEntities.Add(Mapper.Map<GameInfoEntity>(entityToInsert));

            await SaveChangesAsync();
            return Mapper.Map<GameInfo>(entry);
        }

        public async Task<IGameInfo> UpdateAsync(IGameInfo entityToUpdate)
        {
            var entity = Mapper.Map<GameInfoEntity>(entityToUpdate);

            var entry = DbContext.Set<GameInfoEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync();
            return Mapper.Map<GameInfo>(entry);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            var entity = await DbContext.GameInfoEntities.FindAsync(entityKey);

            if (entity != null)
            {
                DbContext.Set<GameInfoEntity>().Attach(entity);
                DbContext.Entry(entity).State = EntityState.Deleted;
                await SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

    }
}
