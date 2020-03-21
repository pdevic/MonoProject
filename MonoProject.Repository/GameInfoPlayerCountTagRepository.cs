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

        public IGameInfoPlayerCountTag GetByID(int gameInfoPlayerCountTagID)
        {
            return _dbContext.GameInfoPlayerCountTags.Find(gameInfoPlayerCountTagID);
        }

        public void Insert(IGameInfoPlayerCountTag entityToInsert)
        {
            _dbContext.GameInfoPlayerCountTags.Add(entityToInsert);
        }

        public void Update(IGameInfoPlayerCountTag entityToUpdate)
        {
            entityToUpdate.DateUpdated = System.DateTime.Now;
        }

        public void Delete(int entityKey)
        {
            _dbContext.GameInfoPlayerCountTags.Remove(GetByID(entityKey));
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
