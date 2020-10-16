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
using MonoProject.Service.Common;

namespace MonoProject.Repository
{
    public class RepositoryBinds : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /*builder.RegisterType<GameInfoRepository>().As<IGameInfoRepository>();
            builder.RegisterType<PlayerCountTagRepository>().As<IPlayerCountTagRepository>();
            builder.RegisterType<GameInfoPlayerCountTagRepository>().As<IGameInfoPlayerCountTagRepository>();*/

            builder.RegisterType<IGameInfoRepository>().As<IBaseRepository<IGameInfo>>();
            builder.RegisterType<IGenreTagRepository>().As<IBaseRepository<IGenreTag>>();
            builder.RegisterType<IGameInfoGenreTagRepository>().As<IBaseRepository<IGameInfoGenreTag>>();

            builder.RegisterType<GameInfoRepository>().As<BaseRepository<IGameInfo, GameInfo, GameInfoEntity>, IGameInfoRepository>();
            builder.RegisterType<GenreTagRepository>().As<BaseRepository<IGenreTag, GenreTag, GenreTagEntity>, IGenreTagRepository>();
            builder.RegisterType<GameInfoGenreTagRepository>().As<BaseRepository<IGameInfoGenreTag, GameInfoGenreTag, GameInfoGenreTagEntity>, IGameInfoGenreTagRepository>();

            builder.RegisterType<GameContext>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
