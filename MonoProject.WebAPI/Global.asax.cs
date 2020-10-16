using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

using AutoMapper;

using MonoProject.Common;
using MonoProject.Model;
using MonoProject.Repository;
using MonoProject.Service;

using static MonoProject.WebAPI.Controllers.GamesController;
using static MonoProject.WebAPI.Controllers.TagsController;

namespace MonoProject.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IContainer Container { get; set; }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterModule<ModelBinds>();
            builder.RegisterModule<RepositoryBinds>();
            builder.RegisterModule<ServiceBinds>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register(ctx => new MapperConfiguration(cfg => {
                cfg.AddProfile(new MonoProject.Repository.MappingProfiles());
                cfg.CreateMap<GameInfo, GameInfoRestBasic>().ReverseMap();
                cfg.CreateMap<GenreTag, GenreTagRestBasic>().ReverseMap();
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();

            Container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
