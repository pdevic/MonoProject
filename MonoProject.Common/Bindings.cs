using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject.Modules;

namespace MonoProject.Common
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailSender>().To<MockMailSender>();
        }
    }
}
