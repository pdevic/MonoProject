using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
//using AutoMapper;

using MonoProject.Repository;
using MonoProject.Service.Common;

namespace MonoProject.Service
{
    public class ServiceBinds : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameInfoService>().As<IGameInfoService>();
            builder.RegisterType<PlayerCountTagService>().As<IPlayerCountTagService>();
            builder.RegisterType<GameInfoPlayerCountTagService>().As<IGameInforPlayerCountTagService>();
        }
    }
}
