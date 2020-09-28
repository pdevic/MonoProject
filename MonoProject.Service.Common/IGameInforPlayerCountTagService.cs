using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;

namespace MonoProject.Service.Common
{
    public interface IGameInforPlayerCountTagService : IRepositoryService<IGameInfoPlayerCountTag, int>
    {
        Task<IEnumerable<IGameInfoPlayerCountTag>> GetListAsync();
    }
}
