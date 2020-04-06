using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject.Modules;

using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class ServiceBinds : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameInfoService>().To<GameInfoService>();
            Bind<IPlayerCountTagService>().To<PlayerCountTagService>();
        }
    }
}
