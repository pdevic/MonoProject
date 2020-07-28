using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.WebPages;

using Autofac;
using Autofac.Integration.Mvc;

using AutoMapper;

using PagedList;

using MonoProject.Common;
using MonoProject.Model;
using MonoProject.Model.Common;
using MonoProject.Service.Common;

namespace MonoProject.WebAPI.Controllers
{
    public class TagsController : Controller
    {
        private readonly IPlayerCountTagService playerCountTagService;
        private readonly IGameInforPlayerCountTagService gameInforPlayerCountTagService;

        public TagsController()
        {
            playerCountTagService = AutofacDependencyResolver.Current.GetService<IPlayerCountTagService>();
            gameInforPlayerCountTagService = AutofacDependencyResolver.Current.GetService<IGameInforPlayerCountTagService>();
        }

        public async Task<ActionResult> Index()
        {
            return View(await playerCountTagService.ListAsync());
        }

        public async Task<ActionResult> CreatePTag(PlayerCountTag newTag)
        {
            if (ModelState.IsValid)
            {
                await playerCountTagService.InsertAsync(newTag);
                return RedirectToAction("Index");
            }

            return View(newTag);
        }

        public async Task<ActionResult> DeletePTag(int tagID)
        {
            var response = await playerCountTagService.DeleteAsync(tagID);

            if (!response)
            {
                ViewBag.ErrorMessage = "Record with ID = " + tagID.ToString() + " not found";
            }
            else
            {
                ViewBag.ErrorMessage = "Deleting successful";
            }

            return View();
        }

        public async Task<IEnumerable<PlayerCountTag>> ResolveTagsAsync(string tags)
        {
            var result = new List<PlayerCountTag>();

            tags = string.Join("", tags.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)); // Removes all whitespace
            var tagStrings = tags.Split(',');

            foreach (string tagName in tagStrings)
            {
                var searchResults = from t in await playerCountTagService.ListAsync() where t.Name.ToLower() == tagName.ToLower() select t;

                if (searchResults.Any())
                {
                    result.Add((PlayerCountTag)searchResults.First());
                }
            }

            return result;
        }

        public async Task<List<GameInfoPlayerCountTag>> CollectGameInfoPlayerCountRelationsAsync(int gameInfoID)
        {
            return (from t in await gameInforPlayerCountTagService.ListAsync() where t.GameInfoID == gameInfoID select (GameInfoPlayerCountTag)t).ToList();
        }

        public async Task<List<string>> CollectGameInfoPlayerCountRelationsTagNamesAsync(int gameInfoID)
        {
            var relationsList = (from t in await gameInforPlayerCountTagService.ListAsync() where t.GameInfoID == gameInfoID select (GameInfoPlayerCountTag)t).ToList();
            var result = new List<string>();

            foreach (GameInfoPlayerCountTag tag in await CollectGameInfoPlayerCountRelationsAsync(gameInfoID))
            {
                result.Add((await playerCountTagService.GetByIDAsync(tag.PlayerCountTagID)).Name);
            }
            return result;
        }

        public async Task<int> CreateGameInfoPlayerCountRelationAsync(int gameInfoID, int playerCountTagID)
        {
            var newRelation = new GameInfoPlayerCountTag();

            newRelation.GameInfoID = gameInfoID;
            newRelation.PlayerCountTagID = playerCountTagID;

            await gameInforPlayerCountTagService.InsertAsync(newRelation);

            return 1;
        }

        public async Task<int> RemoveGameInfoPlayerCountRelationsAsync(int gameInfoID)
        {
            var tagList = await CollectGameInfoPlayerCountRelationsAsync(gameInfoID);

            foreach (var tag in tagList)
            {
                await gameInforPlayerCountTagService.DeleteAsync(tag.ID);
            }

            return tagList.Count();
        }

    }
}
