using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MonoProject.Common;
using MonoProject.DAL;
using MonoProject.Model;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class GameInfoGenreTagRepository : BaseRepository<IGameInfoGenreTag, GameInfoGenreTag, GameInfoGenreTagEntity>, IGameInfoGenreTagRepository
    {
        public GameInfoGenreTagRepository(GameContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }
        public async Task<IEnumerable<IGameInfoGenreTag>> GetByGameIDAsync(int gameID)
        {
            var query = (await GetEntitySet().ToListAsync()).Where(x => x.GameInfoID == gameID);

            return Mapper.Map<List<GameInfoGenreTag>>(query);
        }

    }
}
