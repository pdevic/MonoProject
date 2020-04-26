using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Autofac;
using Autofac.Integration.Mvc;

using MonoProject.Model.Common;

using MonoProject.Model;
using MonoProject.Repository;
using MonoProject.Service;

namespace MonoProject.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IContainer container { get; set; }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ModelBinds>();
            builder.RegisterModule<RepositoryBinds>();
            builder.RegisterModule<ServiceBinds>();
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);

            container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
