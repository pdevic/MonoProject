using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MonoProject.Service.Common;

namespace MonoProject.WebAPI.Controllers
{
    public class GameInfoController : Controller
    {
        private readonly IGameInfoService gameInfoService;

        public ActionResult Index()
        {
            return View();
        }
    }
}