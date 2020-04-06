using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject.Modules;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class ModelBinds : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameInfo>().To<GameInfo>();
            Bind<IPlayerCountTag>().To<PlayerCountTag>();
            Bind<IGameInfoPlayerCountTag>().To<GameInfoPlayerCountTag>();
        }
    }
}
