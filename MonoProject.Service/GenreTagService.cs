using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Common;
using MonoProject.Model.Common;
using MonoProject.Repository;
using MonoProject.Repository.Common;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class GenreTagService : GenericService<IGenreTag, IGenreTagRepository>, IGenreTagService
    {
        public GenreTagService(IGenreTagRepository _repository) : base(_repository)
        {

        }

        public async Task<IGenreTag> GetByNameAsync(string tagName)
        {
            return await Repository.GetByNameAsync(tagName);
        }

        public async Task<List<string>> FilterInvalidTags(List<string> tags)
        {
            List<string> invalidTags = new List<string>();

            foreach(var tagName in tags)
            {
                if ((await Repository.GetByNameAsync(tagName)) == null)
                {
                    invalidTags.Add(tagName);
                }
            }

            return invalidTags;
        }

    }
}
