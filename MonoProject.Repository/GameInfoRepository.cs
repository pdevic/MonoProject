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

        public IEnumerable<IGameInfo> Get()
        {
            return _dbContext.GameInfos.ToList();
        }

        public Task<IEnumerable<IGameInfo>> GetAsync()
        {
            return new Task<IEnumerable<IGameInfo>>(Get);
        }

        public IGameInfo GetByID(int gameInfoID)
        {
            return _dbContext.GameInfos.Find(gameInfoID);
        }

        public Task<IGameInfo> GetByIDAsync(int gameInfoID)
        {
            return _dbContext.GameInfos.FindAsync(gameInfoID);
        }

        /*public IEnumerable<IGameInfo> Find(string name)
        {
            return _dbContext.GameInfos.Where(x => x.Name.StartsWith(name)).ToList<IGameInfo>();
        }*/

        public void Insert(IGameInfo entityToInsert)
        {
            _dbContext.GameInfos.Add(entityToInsert);
        }

        public void Update(IGameInfo entityToUpdate)
        {
            _dbContext.GameInfos.Attach(entityToUpdate);
            var entry = _dbContext.Entry(entityToUpdate);

            entry.Entity.DateUpdated = System.DateTime.Now;
        }

        /*public void Delete(IGameInfo entityToDelete)
        {
            _dbContext.GameInfos.Remove(entityToDelete);
        }*/

        public void Delete(int entityKey)
        {
            _dbContext.GameInfos.Remove(GetByID(entityKey));
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
