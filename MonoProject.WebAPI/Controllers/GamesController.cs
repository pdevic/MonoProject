using System;
using System.Threading.Tasks;
using MonoProject.Model;
using MonoProject.Service.Common;

using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Text.Json;
using System.Text.Json.Serialization;

using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using MonoProject.Common;
using System.Web.Helpers;
using System.Web;
using Newtonsoft.Json;

namespace MonoProject.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameInfoService GameInfoService;
        private readonly IMapper Mapper;



        public class GameInfoRestBasic
        {
            [Required] public int ID { get; set; }
            [Required] public string Name { get; set; }
            [Required] public string Description { get; set; }
            [Required] public DateTime ReleaseDate { get; set; }
        }



        public GamesController(IGameInfoService gameInfoService, IMapper mapper)
        {
            GameInfoService = gameInfoService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("index")]
        public async Task<HttpResponseMessage> Index([FromUri] PagingParameterModel pagingParameterModel, [FromUri] SortingParameterModel sortingParameterModel)
        {
            if (pagingParameterModel == null)
            {
                pagingParameterModel = new PagingParameterModel();    
            }

            if (sortingParameterModel == null)
            {
                sortingParameterModel = new SortingParameterModel();
            }

            if (!SortingParameterModel.OrderByOptions.Contains(sortingParameterModel.OrderBy))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unknown sorting parameter " + sortingParameterModel.OrderBy);
            }

            if (!SortingParameterModel.SortingOrderOptions.Contains(sortingParameterModel.SortingOrder))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unknown sorting order parameter " + sortingParameterModel.SortingOrder);
            }

            var res = Mapper.Map<List<GameInfoRestBasic>>(await GameInfoService.GetListAsync(pagingParameterModel, sortingParameterModel));
            var totalItemsCount = await GameInfoService.GetAllCountAsync();

            var pagingMetadata = new
            {
                TotalItemsCount = totalItemsCount,
                CurrentPage = pagingParameterModel.PageNumber,
                PageSize = pagingParameterModel.PageSize,
                TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pagingParameterModel.PageSize)
            };

            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(pagingMetadata));
            HttpContext.Current.Response.Headers.Add("Sorting-Headers", JsonConvert.SerializeObject(sortingParameterModel));

            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        [Route("get/{gameID}")]
        public async Task<HttpResponseMessage> Get(int gameID)
        {
            var entity = await GameInfoService.GetByIDAsync(gameID);

            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Entry with requested id doesn't exist in the database");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<GameInfoRestBasic>(entity));
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<HttpResponseMessage> Create(GameInfo newGame)
        {
            if (ModelState.IsValid)
            {
                var res = Mapper.Map<GameInfoRestBasic>(await GameInfoService.InsertAsync(newGame));
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "null or invalid object given to create action");
        }

        [HttpPut]
        [Route("edit")]
        public async Task<HttpResponseMessage> Edit(GameInfoRestBasic gameToUpdateRest)
        {
            var original = await GameInfoService.GetByIDAsync(gameToUpdateRest.ID);

            if (original != null)
            {
                var gameToUpdate = Mapper.Map<GameInfo>(gameToUpdateRest);

                gameToUpdate.DateCreated = original.DateCreated;
                gameToUpdate.DateUpdated = System.DateTime.Now;
                gameToUpdate.TimeStamp = System.DateTime.Now;

                var res = Mapper.Map<GameInfoRestBasic>(await GameInfoService.UpdateAsync(gameToUpdate));
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid object or incorrect id given to update action ");
        }

        [HttpDelete]
        [Route("delete/{gameID}")]
        public async Task<HttpResponseMessage> Delete(int gameID)
        {
            var result = await GameInfoService.DeleteAsync(gameID);

            if (!result)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entry with the requested id exists in the database");
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}