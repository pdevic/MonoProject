using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using MonoProject.Model;

namespace MonoProject.DAL
{
    public class Startup
    {
        public static IContainer container { get; set; }

        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ModelBinds>();

            container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
