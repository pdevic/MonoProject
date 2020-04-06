using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject.Modules;

using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class RepositoryBinds : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameInfoRepository>().To<GameInfoRepository>();
            Bind<IPlayerCountTagRepository>().To<PlayerCountTagRepository>();
            Bind<IGameInfoPlayerCountTagRepository>().To<GameInfoPlayerCountTagRepository>();
        }
    }
}
