using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MonoProject.DAL;
using MonoProject.Model;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class GameInfoPlayerCountTagRepository : IGameInfoPlayerCountTagRepository
    {
        private readonly GameContext DbContext;
        private readonly IMapper Mapper;
        public GameInfoPlayerCountTagRepository(GameContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public async Task<IGameInfoPlayerCountTag> GetByIDAsync(int gameInfoPlayerCountTagID)
        {
            return Mapper.Map<GameInfoPlayerCountTag>(await DbContext.GameInfoPlayerCountTagEntities.AsNoTracking().SingleOrDefaultAsync(x => x.ID == gameInfoPlayerCountTagID));
        }

        public async Task<IEnumerable<IGameInfoPlayerCountTag>> GetListAsync()
        {
            return Mapper.Map<List<GameInfoPlayerCountTag>>(await DbContext.GameInfoPlayerCountTagEntities.ToListAsync());
        }

        public async Task<IGameInfoPlayerCountTag> InsertAsync(IGameInfoPlayerCountTag entityToInsert)
        {
            var entry = DbContext.GameInfoPlayerCountTagEntities.Add(Mapper.Map<GameInfoPlayerCountTagEntity>(entityToInsert));

            await SaveChangesAsync();
            return Mapper.Map<GameInfoPlayerCountTag>(entry);
        }

        public async Task<IGameInfoPlayerCountTag> UpdateAsync(IGameInfoPlayerCountTag entityToUpdate)
        {
            var entity = Mapper.Map<GameInfoPlayerCountTagEntity>(entityToUpdate);

            var entry = DbContext.Set<GameInfoPlayerCountTagEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync();
            return Mapper.Map<GameInfoPlayerCountTag>(entry);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            var entity = await DbContext.GameInfoPlayerCountTagEntities.FindAsync(entityKey);

            if (entity != null)
            {
                DbContext.Set<GameInfoPlayerCountTagEntity>().Attach(entity);
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
