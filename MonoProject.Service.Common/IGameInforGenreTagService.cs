using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;

namespace MonoProject.Service.Common
{
    public interface IGameInforGenreTagService : IRepositoryService<IGameInfoGenreTag, int>
    {
        IGenreTagService GetGenreTagService();

        Task<IEnumerable<IGameInfoGenreTag>> GetByGameIDAsync(int gameID);
        Task<List<string>> GetGameTagsAsync(int gameID);
    }
}
