using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;

namespace MonoProject.Repository.Common
{
    public interface IGenreTagRepository : IBaseRepository<IGenreTag>
    {
        Task<IGenreTag> GetByNameAsync(string tagName);
    }
}
