using MonoProject.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoProject.Repository.Common
{
    public interface IGameInfoRepository : IBaseRepository<IGameInfo>
    {
        Task<IEnumerable<IGameInfo>> FilterByNameAsync(string nameQuery);
    }
}
