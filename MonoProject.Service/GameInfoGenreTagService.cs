using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MonoProject.Common;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class GameInfoGenreTagService : GenericService<IGameInfoGenreTag, IGameInfoGenreTagRepository>, IGameInforGenreTagService
    {
        public readonly IGenreTagService GenreTagServiceInstance;

        public GameInfoGenreTagService(IGameInfoGenreTagRepository _repository, IGenreTagService genreTagService) : base(_repository)
        {
            GenreTagServiceInstance = genreTagService;
        }

        public IGenreTagService GetGenreTagService()
        {
            return GenreTagServiceInstance;
        }

        public async Task<IEnumerable<IGameInfoGenreTag>> GetByGameIDAsync(int gameID)
        {
            return await Repository.GetByGameIDAsync(gameID);
        }

        public async Task<List<string>> GetGameTagsAsync(int gameID)
        {
            var gameTags = await Repository.GetByGameIDAsync(gameID);
            List<string> res = new List<string>();

            foreach (var gameTag in gameTags)
            {
                res.Add((await GenreTagServiceInstance.GetByIDAsync(gameTag.GenreTagID)).Name);
            }

            return res;
        }

    }
}
