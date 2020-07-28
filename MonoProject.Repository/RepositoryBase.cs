using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;

using MonoProject.DAL;
using MonoProject.Repository.Common;
using System.Data.Entity;

namespace MonoProject.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity, int>
        where TEntity : class, IPoco
    {
        private readonly GameContext context;

        public RepositoryBase(GameContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> InsertAsync(TEntity entityToInsert)
        {
            context.Set<TEntity>().Add(entityToInsert);
            await context.SaveChangesAsync();
            return entityToInsert;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<TEntity> GetByIDAsync(int entityKey)
        {
            return await context.Set<TEntity>().FindAsync(entityKey);
        }

        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        /*public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }*/

        public async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            //context.Entry(entity).State = EntityState.Modified;
            entityToUpdate.DateUpdated = System.DateTime.Now;
            await context.SaveChangesAsync();
            return entityToUpdate;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
