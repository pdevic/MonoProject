using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using MonoProject.Model;
using MonoProject.Service.Common;

namespace MonoProject.WebAPI.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameInfoService gameInfoService;

        public GamesController()
        {
            gameInfoService = AutofacDependencyResolver.Current.GetService<IGameInfoService>();
        }

        public ActionResult Index()
        {
            return View(gameInfoService.TestList());
        }

        public async Task<ActionResult> Inspect(int gameID)
        {
            var entity = await gameInfoService.GetByIDAsync(gameID);

            if (entity == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(entity);
            }
        }

        public async Task<ActionResult> Create(GameInfo newGame)
        {
            if (ModelState.IsValid)
            {
                await gameInfoService.InsertAsync(newGame);
                return RedirectToAction("Index");
            }
            else
            {
                return View(newGame);
            }
        }

        public async Task<ActionResult> Delete(int gameID)
        {
            var response = await gameInfoService.DeleteAsync(gameID);

            if (!response)
            {
                ViewBag.ErrorMessage = "Record with ID = " + gameID.ToString() + " not found";
            }
            else
            {
                ViewBag.ErrorMessage = "Deleting successful";
            }

            return View();
        }

    }
}