using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MonoProject.Common;
using MonoProject.DAL;
using MonoProject.Model;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class PlayerCountTagRepository : IPlayerCountTagRepository
    {
        private readonly GameContext DbContext;
        private readonly IMapper Mapper;

        public PlayerCountTagRepository(GameContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public async Task<int> GetAllCountAsync()
        {
            return Mapper.Map<List<PlayerCountTag>>(await DbContext.PlayerCountTagEntities.ToListAsync()).Count();
        }
        public async Task<IPlayerCountTag> GetByIDAsync(int playerCountTagID)
        {
            return Mapper.Map<PlayerCountTag>(await DbContext.PlayerCountTagEntities.AsNoTracking().SingleOrDefaultAsync(x => x.ID == playerCountTagID));
        }

        public async Task<IEnumerable<IPlayerCountTag>> GetListAsync(PagingParameterModel pagingParameterModel, SortingParameterModel sortingParameterModel)
        {
            return Mapper.Map<List<PlayerCountTag>>(await DbContext.PlayerCountTagEntities.ToListAsync());
        }

        public async Task<IPlayerCountTag> InsertAsync(IPlayerCountTag entityToInsert)
        {
            var entry = DbContext.PlayerCountTagEntities.Add(Mapper.Map<PlayerCountTagEntity>(entityToInsert));

            await SaveChangesAsync();
            return Mapper.Map<PlayerCountTag>(entry);
        }

        public async Task<IPlayerCountTag> UpdateAsync(IPlayerCountTag entityToUpdate)
        {
            var entity = Mapper.Map<PlayerCountTagEntity>(entityToUpdate);

            var entry = DbContext.Set<PlayerCountTagEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync();
            return Mapper.Map<PlayerCountTag>(entry);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            var entity = await DbContext.PlayerCountTagEntities.FindAsync(entityKey);

            if (entity != null)
            {
                DbContext.Set<PlayerCountTagEntity>().Attach(entity);
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
