using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public override async Task<IEnumerable<IGameInfo>> GetListAsync(PagingParameterModel pagingParameterModel, SortingParameterModel sortingParameterModel, SearchParameters searchParameters)
        {
            var query = (await GetEntitySet().ToListAsync()).AsQueryable();

            if (searchParameters.NameQuery != "")
            {
                query = query.Where(x => x.Name.ToLower().Contains(searchParameters.NameQuery.ToLower()));
            }

            if (searchParameters.TagsQuery != "")
            {
                List<string> tagsList = searchParameters.TagsQuery.Split(',').ToList();
                List<int> tagIDs = new List<int>();

                foreach(string tagName in tagsList)
                {
                    tagIDs.Add((await Context.GenreTagEntities.Where(x => x.Name == tagName).FirstAsync()).ID);
                }

                // This query does the following (for each game in query as x):
                // 1) Collect all relations for x
                // 2) Collect all relations whose GenreTagIDs are contained in the requested list of tags
                // 3) Count the matching relations, if all of the requested tags are linked to x then the amount is equal to the amount of requested tags
                query = query.Where(x => Context.GameInfoGenreTagEntities.Where(y => y.GameInfoID == x.ID).Where(z => tagIDs.Contains(z.GenreTagID)).Count() == tagIDs.Count);
            }

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

            int skip = (pagingParameterModel.PageNumber - 1) * pagingParameterModel.PageSize;
            return Mapper.Map<List<GameInfo>>(query.Skip(skip).Take(pagingParameterModel.PageSize).ToList());
        }

        public async Task<IEnumerable<IGameInfo>> FilterByNameAsync(string nameQuery)
        {
            var query = (await GetEntitySet().ToListAsync()).AsQueryable();
            query = query.Where(x => x.Name.Contains(nameQuery));

            return Mapper.Map<List<GameInfo>>(query.ToList());
        }

    }
}
