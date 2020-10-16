using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

using AutoMapper;

using MonoProject.Common;
using MonoProject.DAL;
using MonoProject.Model;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class GameInfoRepository : BaseRepository<IGameInfo, GameInfo, GameInfoEntity>, IGameInfoRepository
    {
        public GameInfoRepository(GameContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public override async Task<IEnumerable<IGameInfo>> GetListAsync(PagingParameterModel pagingParameterModel, SortingParameterModel sortingParameterModel)
        {
            var query = (await GetEntitySet().ToListAsync()).AsQueryable();

            if (sortingParameterModel.OrderBy == "Name")
            {
                query = query.OrderBy(x => x.Name);
            }
            else
            {
                query = query.OrderBy(x => x.ReleaseDate);
            }

            if (sortingParameterModel.SortingOrder == "Desc")
            {
                query = query.Reverse();
            }

            return Mapper.Map<List<GameInfo>>(query.Skip((pagingParameterModel.PageNumber - 1) * pagingParameterModel.PageSize).Take(pagingParameterModel.PageSize).ToList());
        }

    }
}
