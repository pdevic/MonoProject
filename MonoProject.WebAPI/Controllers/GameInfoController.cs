using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using MonoProject.Model.Common;
using MonoProject.Service.Common;

namespace MonoProject.WebAPI.Controllers
{
    public class GameInfoController : Controller
    {
        private readonly IGameInfoService gameInfoService;

        public GameInfoController()
        {
            gameInfoService = AutofacDependencyResolver.Current.GetService<IGameInfoService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        /*public ActionResult Welcome(int GameInfoID)
        {
            return View();
        }*/
    }
}