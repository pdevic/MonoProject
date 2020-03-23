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

        public IEnumerable<IPlayerCountTag> Get()
        {
            return _dbContext.PlayerCountTags.ToList();
        }

        public Task<IEnumerable<IPlayerCountTag>> GetAsync()
        {
            return new Task<IEnumerable<IPlayerCountTag>>(Get);
        }

        public IPlayerCountTag GetByID(int playerCountTagID)
        {
            return _dbContext.PlayerCountTags.Find(playerCountTagID);
        }

        public Task<IPlayerCountTag> GetByIDAsync(int playerCountTagID)
        {
            return _dbContext.PlayerCountTags.FindAsync(playerCountTagID);
        }

        public void Insert(IPlayerCountTag entityToInsert)
        {
            _dbContext.PlayerCountTags.Add(entityToInsert);
        }

        public void Update(IPlayerCountTag entityToUpdate)
        {
            _dbContext.PlayerCountTags.Attach(entityToUpdate);
            var entry = _dbContext.Entry(entityToUpdate);

            entry.Entity.DateUpdated = System.DateTime.Now;
        }

        public void Delete(int entityKey)
        {
            _dbContext.PlayerCountTags.Remove(GetByID(entityKey));
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
