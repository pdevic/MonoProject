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
    public class GenreTagRepository : BaseRepository<IGenreTag, GenreTag, GenreTagEntity>, IGenreTagRepository
    {
        public GenreTagRepository(GameContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public override async Task<IEnumerable<IGenreTag>> GetListAsync(PagingParameterModel pagingParameterModel, SortingParameterModel sortingParameterModel, SearchParameters searchParameters)
        {
            var query = (await GetEntitySet().ToListAsync()).AsQueryable();

            if (sortingParameterModel.OrderBy == "Name")
            {
                query = query.OrderBy(x => x.Name);
            }

            if (sortingParameterModel.SortingOrder == "Desc")
            {
                query = query.Reverse();
            }

            return Mapper.Map<List<GenreTag>>(query.Skip((pagingParameterModel.PageNumber - 1) * pagingParameterModel.PageSize).Take(pagingParameterModel.PageSize).ToList());
        }

        public async Task<IGenreTag> GetByNameAsync(string tagName)
        {
            return Mapper.Map<GenreTag>(await GetEntitySet().FirstOrDefaultAsync(x => x.Name == tagName));
        }

    }
}
