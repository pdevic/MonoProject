﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;

namespace MonoProject.Repository.Common
{
    public interface IBaseRepository<TInterface> : IRepository<TInterface, int>
        where TInterface : class
    {

    }
}
