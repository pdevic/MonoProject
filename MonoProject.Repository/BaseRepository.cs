using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MonoProject.Common;
using MonoProject.DAL;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;
using MonoProject.Service.Common;

namespace MonoProject.Repository
{
    public abstract class BaseRepository<TInterface, TClass, TEntity> : IBaseRepository<TInterface>
        where TInterface : class
        where TClass : class, TInterface
        where TEntity : class, IPoco
    {
        protected readonly GameContext Context;
        protected readonly IMapper Mapper;

        public BaseRepository(GameContext dbContext, IMapper mapper)
        {
            Context = dbContext;
            Mapper = mapper;
        }

        public async Task<TInterface> GetByIDAsync(int gameInfoPlayerCountTagID)
        {
            return Mapper.Map<TClass>(await Context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.ID == gameInfoPlayerCountTagID));
        }

        public virtual async Task<IEnumerable<TInterface>> GetListAsync(PagingParameterModel pagingParameterModel, SortingParameterModel sortingParameterModel, SearchParameters searchParameters)
        {
            var query = (await GetEntitySet().ToListAsync()).AsQueryable();
            return Mapper.Map<List<TClass>>(query.Skip((pagingParameterModel.PageNumber - 1) * pagingParameterModel.PageSize).Take(pagingParameterModel.PageSize).ToList());
        }

        public async Task<int> GetAllCountAsync()
        {
            return Mapper.Map<List<TClass>>(await Context.Set<TEntity>().ToListAsync()).Count();
        }
        

        protected System.Data.Entity.DbSet<TEntity> GetEntitySet()
        {
            return Context.Set<TEntity>();
        }

        public async Task<TInterface> InsertAsync(TInterface entityToInsert)
        {
            var entry = Context.Set<TEntity>().Add(Mapper.Map<TEntity>(entityToInsert));

            await SaveChangesAsync();
            return Mapper.Map<TClass>(entry);
        }

        public async Task<TInterface> UpdateAsync(TInterface entityToUpdate)
        {
            var entity = Mapper.Map<TEntity>(entityToUpdate);

            var entry = Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync();
            return Mapper.Map<TClass>(entry);
        }

        public async Task<bool> DeleteAsync(int entityKey)
        {
            var entity = await Context.Set<TEntity>().FindAsync(entityKey);

            if (entity != null)
            {
                Context.Set<TEntity>().Attach(entity);
                Context.Entry(entity).State = EntityState.Deleted;
                await SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
