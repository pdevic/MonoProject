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
            builder.RegisterType<GenreTagService>().As<IGenreTagService>();
            builder.RegisterType<GameInfoGenreTagService>().As<IGameInforGenreTagService>();
        }
    }
}
