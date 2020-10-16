using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Common;
using MonoProject.Model.Common;

namespace MonoProject.Service.Common
{
    public interface IGameInfoService : IRepositoryService<IGameInfo, int>
    {
        Task<IEnumerable<IGameInfo>> FilterByNameAsync(string nameQuery);
    }
}
