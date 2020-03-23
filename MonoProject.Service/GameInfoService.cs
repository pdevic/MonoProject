using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;
using MonoProject.Repository.Common;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class GameInfoService : IGameInfoService
    {
        private readonly IGameInfoRepository repository;

        public GameInfoService(IGameInfoRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IGameInfo> GetByID(int entityKey)
        {
            return await repository.GetByIDAsync(entityKey);
        }
    }
}
