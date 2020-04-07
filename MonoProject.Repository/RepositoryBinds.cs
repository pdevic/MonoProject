using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using MonoProject.Model;
using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class RepositoryBinds
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            ModelBinds.RegisterTypes(builder);

            builder.RegisterType<GameInfoRepository>().As<IGameInfoRepository>();
            builder.RegisterType<PlayerCountTagRepository>().As<PlayerCountTagRepository>();
            builder.RegisterType<GameInfoPlayerCountTagRepository>().As<GameInfoPlayerCountTagRepository>();
        }
    }
}
