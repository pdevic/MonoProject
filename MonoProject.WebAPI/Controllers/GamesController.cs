using System;
using System.Threading.Tasks;
using MonoProject.Model;
using MonoProject.Service.Common;

using System.Net;
using System.Net.Http;
using System.Web.Http;

using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace MonoProject.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameInfoService gameInfoService;

        public GamesController(IGameInfoService gameInfoService)
        {
            this.gameInfoService = gameInfoService;
        }

        [HttpGet]
        [Route("index")]
        public async Task<HttpResponseMessage> Index()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await gameInfoService.GetListAsync());
        }

        [HttpGet]
        [Route("get/{gameID}")]
        public async Task<HttpResponseMessage> Get(int gameID)
        {
            var entity = await gameInfoService.GetByIDAsync(gameID);

            if (entity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<HttpResponseMessage> Create(GameInfo newGame)
        {
            if (ModelState.IsValid)
            {
                await gameInfoService.InsertAsync(newGame);

                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri("~/api/games/get/" + newGame.ID.ToString());

                return response;
            }

            return Request.CreateResponse(HttpStatusCode.OK, newGame);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<HttpResponseMessage> Edit(GameInfo gameToUpdate)
        {
            if (ModelState.IsValid)
            {
                await gameInfoService.UpdateAsync(gameToUpdate);

                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri("~/api/games/get/" + gameToUpdate.ID.ToString());

                return response;
            }

            return Request.CreateResponse(HttpStatusCode.OK, gameToUpdate);
        }

        [HttpDelete]
        [Route("delete/{gameID}")]
        public async Task<HttpResponseMessage> Delete(int gameID)
        {
            var result = await gameInfoService.DeleteAsync(gameID);

            if (!result)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}