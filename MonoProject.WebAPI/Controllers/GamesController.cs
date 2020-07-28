using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.WebPages;

using System.ComponentModel.DataAnnotations;

using Autofac;
using Autofac.Integration.Mvc;

using AutoMapper;

using PagedList;

using MonoProject.Common;
using MonoProject.Model;
using MonoProject.Model.Common;
using MonoProject.Service.Common;
using MonoProject.Service;
using MonoProject.Repository;

namespace MonoProject.WebAPI.Controllers
{
    [Route("/api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameInfoService gameInfoService;
        private readonly IMapper mapper;

        public GamesController()
        {
            gameInfoService = AutofacDependencyResolver.Current.GetService<IGameInfoService>();
            var mapperCfg = new MapperConfiguration(cfg => cfg.CreateMap<GameInfo, GameInforQueryResource>());

            mapper = new Mapper(mapperCfg);
        }

        public async Task<ActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IEnumerable<IGameInfo> content = from s in await gameInfoService.ListAsync() select s;

            if (!searchString.IsEmpty())
            {
                content = content.Where(s => s.Name.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date":
                    content = content.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    content = content.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "name_desc":
                    content = content.OrderByDescending(s => s.Name);
                    break;
                default:
                    content = content.OrderBy(s => s.Name);
                    break;
            }

            int pageNumber = (page ?? 1);

            var result = mapper.Map<IEnumerable<IGameInfo>, IEnumerable<GameInforQueryResource>>(content);
            return View(result.ToPagedList(pageNumber, 10));
        }

        [HttpGet]
        public async Task<ActionResult> Get(int gameID)
        {
            var entity = await gameInfoService.GetByIDAsync(gameID);

            if (entity == null)
            {
                return HttpNotFound();
            }
            else
            {
                var tagsController = DependencyResolver.Current.GetService<TagsController>();
                tagsController.ControllerContext = new ControllerContext(this.Request.RequestContext, tagsController);

                ViewBag.Tags = await tagsController.CollectGameInfoPlayerCountRelationsTagNamesAsync(gameID);

                return View(mapper.Map<GameInforQueryResource>(entity));
            }
        }

        /*[HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var content = await gameInfoService.ListAsync();
            var result = mapper.Map<IEnumerable<IGameInfo>, IEnumerable<GameInforQueryResource>>(content);

            return View(result);
        }*/

        /*[HttpGet]
        public async Task<ActionResult> GetRaw(int gameID)
        {
            var entity = await gameInfoService.GetByIDAsync(gameID);

            if (entity == null)
            {
                return null;
            }
            else
            {
                return Json(mapper.Map<GameInforQueryResource>(entity), JsonRequestBehavior.AllowGet); ;
            }
        }*/

        public async Task<ActionResult> Create(GameInfo newGame, string tags)
        {
            if (ModelState.IsValid)
            {
                await gameInfoService.InsertAsync(newGame);
                await UpdateTagsAsync(newGame, tags);
                return RedirectToAction("Get", new { gameID = newGame.ID });
            }

            return View(newGame);
        }

        public async Task<ActionResult> Edit(int gameID)
        {
            var gameToUpdate = await gameInfoService.GetByIDAsync(gameID);

            if (gameToUpdate == null)
            {
                return HttpNotFound();
            }
            else
            {
                var tagsController = DependencyResolver.Current.GetService<TagsController>();
                tagsController.ControllerContext = new ControllerContext(this.Request.RequestContext, tagsController);

                ViewBag.Tags = await tagsController.CollectGameInfoPlayerCountRelationsTagNamesAsync(gameID);

                return View(gameToUpdate);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(GameInfo gameToUpdate, string tags)
        {
            if (ModelState.IsValid)
            {
                await gameInfoService.UpdateAsync(gameToUpdate);
                await UpdateTagsAsync(gameToUpdate, tags);
                return RedirectToAction("Get", new { gameID = gameToUpdate.ID });
            }

            return View(gameToUpdate);
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

        public async Task<int> UpdateTagsAsync(GameInfo target, string tags)
        {
            var tagsController = DependencyResolver.Current.GetService<TagsController>();
            tagsController.ControllerContext = new ControllerContext(this.Request.RequestContext, tagsController);

            await tagsController.RemoveGameInfoPlayerCountRelationsAsync(target.ID);

            var targetTags = await tagsController.ResolveTagsAsync(tags);

            foreach (PlayerCountTag tag in targetTags.ToList())
            {
                await tagsController.CreateGameInfoPlayerCountRelationAsync(target.ID, tag.ID);
            }

            return targetTags.Count();
        }

    }
}