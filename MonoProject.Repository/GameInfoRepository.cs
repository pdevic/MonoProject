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

        public IEnumerable<IGameInfo> GetAll()
        {
            return _dbContext.GameInfos.ToList();
        }

        public IGameInfo GetByID(int GameInfoID)
        {
            return _dbContext.GameInfos.Find(GameInfoID);
        }

        public IEnumerable<IGameInfo> Find(string name)
        {
            return _dbContext.GameInfos.Where(x => x.Name.StartsWith(name)).ToList<IGameInfo>();
        }

        public void Insert(IGameInfo entityToInsert)
        {
            _dbContext.GameInfos.Attach(entityToInsert);
        }

        public void Update(IGameInfo entityToUpdate)
        {
            entityToUpdate.DateUpdated = System.DateTime.Now;
        }

        public void Delete(IGameInfo entityToDelete)
        {
            _dbContext.GameInfos.Remove(entityToDelete);
        }

        public void Delete(int entityKey)
        {
            Delete(GetByID(entityKey));
        }
    }
}
