using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Common;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class GameInfoService : GenericService<IGameInfo, IGameInfoRepository>, IGameInfoService
    {
        public GameInfoService(IGameInfoRepository _repository) : base(_repository)
        {

        }

        public async Task<IEnumerable<IGameInfo>> FilterByNameAsync(string nameQuery)
        {
            return await Repository.FilterByNameAsync(nameQuery);
        }
    }
}
