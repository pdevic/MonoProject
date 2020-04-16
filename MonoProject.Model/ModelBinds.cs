﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
//using AutoMapper;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class ModelBinds : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameInfo>().As<IGameInfo>();
            builder.RegisterType<PlayerCountTag>().As<IPlayerCountTag>();
            builder.RegisterType<GameInfoPlayerCountTag>().As<IGameInfoPlayerCountTag>();
        }
    }
}
