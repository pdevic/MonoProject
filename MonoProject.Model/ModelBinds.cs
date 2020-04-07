using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class ModelBinds
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<GameInfo>().As<IGameInfo>();
            builder.RegisterType<PlayerCountTag>().As<PlayerCountTag>();
            builder.RegisterType<GameInfoPlayerCountTag>().As<GameInfoPlayerCountTag>();
        }
    }
}
