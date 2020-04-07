using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using MonoProject.Repository;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class ServiceBinds
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            RepositoryBinds.RegisterTypes(builder);

            builder.RegisterType<GameInfoService>().As<IGameInfoService>();
            builder.RegisterType<PlayerCountTagService>().As<PlayerCountTagService>();
        }
    }
}
