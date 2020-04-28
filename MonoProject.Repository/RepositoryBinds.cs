using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
//using AutoMapper;

using MonoProject.DAL;
using MonoProject.Model;
using MonoProject.Model.Common;
using MonoProject.Repository.Common;

namespace MonoProject.Repository
{
    public class RepositoryBinds : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameInfoRepository>().As<IGameInfoRepository>();
            builder.RegisterType<PlayerCountTagRepository>().As<IPlayerCountTagRepository>();
            builder.RegisterType<GameInfoPlayerCountTagRepository>().As<IGameInfoPlayerCountTagRepository>();
            builder.RegisterType<GameContext>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
