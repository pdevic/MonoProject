﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;

namespace MonoProject.Service.Common
{
    public interface IGenreTagService : IRepositoryService<IGenreTag, int>
    {
        Task<IGenreTag> GetByNameAsync(string tagName);
        Task<List<string>> FilterInvalidTags(List<string> tags);
    }
}
