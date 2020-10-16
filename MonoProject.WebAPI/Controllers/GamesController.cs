using System;
using System.Threading.Tasks;
using MonoProject.Model;
using MonoProject.Service.Common;

using System.Net;
using System.Net.Http;
using System.Web.Http;

using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Web;
using Newtonsoft.Json;

using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

using MonoProject.Common;
using MonoProject.Model.Common;
using MonoProject.Service;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Linq;
using Autofac.Core;
using Microsoft.Ajax.Utilities;

namespace MonoProject.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameInfoService GameInfoServiceInstance;
        private readonly IGameInforGenreTagService GameInfoGenreTagServiceInstance;
        private readonly IMapper Mapper;

        public class GameInfoRestBasic
        {
            [Required] public int ID { get; set; }
            [Required] public string Name { get; set; }
            [Required] public string Description { get; set; }
            [Required] public DateTime ReleaseDate { get; set; }
            [Required] public List<string> Tags { get; set; }
        }

        public GamesController(IGameInfoService gameInfoService, IGameInforGenreTagService gameInfoGenreTagService, IMapper mapper)
        {
            GameInfoServiceInstance = gameInfoService;
            GameInfoGenreTagServiceInstance = gameInfoGenreTagService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("index")]
        public async Task<HttpResponseMessage> IndexAsync([FromUri] PagingParameterModel pagingParameterModel, [FromUri] SortingParameterModel sortingParameterModel)
        {
            Common.Common.FillEmptyParameters(pagingParameterModel, sortingParameterModel);

            if (!SortingParameterModel.OrderByOptions.Contains(sortingParameterModel.OrderBy))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unknown sorting parameter " + sortingParameterModel.OrderBy);
            }

            if (!SortingParameterModel.SortingOrderOptions.Contains(sortingParameterModel.SortingOrder))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unknown sorting order parameter " + sortingParameterModel.SortingOrder);
            }

            var res = Mapper.Map<List<GameInfoRestBasic>>(await GameInfoServiceInstance.GetListAsync(pagingParameterModel, sortingParameterModel));
            var totalItemsCount = await GameInfoServiceInstance.GetAllCountAsync();

            foreach (var game in res)
            {
                await FillGameTags(game);
            }

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
        public async Task<HttpResponseMessage> GetAsync(int gameID)
        {
            var entity = await GameInfoServiceInstance.GetByIDAsync(gameID);

            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Entry with requested id doesn't exist in the database");
            }
            else
            {
                var game = Mapper.Map<GameInfoRestBasic>(entity);
                await FillGameTags(game);

                return Request.CreateResponse(HttpStatusCode.OK, game);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<HttpResponseMessage> CreateAsync([FromBody] GameInfoRestBasic newGameRest)
        {
            var newGame = Mapper.Map<GameInfo>(newGameRest);
            var nameValidation = ValidateName(newGame);

            if (nameValidation != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, nameValidation.ErrorMessage);
            }

            if (newGame.ReleaseDate == default)
            {
                newGame.ReleaseDate = System.DateTime.Now;
            }

            if (newGame.Description == null)
            {
                newGame.Description = "";
            }

            var res = Mapper.Map<GameInfoRestBasic>(await GameInfoServiceInstance.InsertAsync(newGame));

            if (newGameRest.Tags != null)
            {
                await UpdateTagsAsync(res.ID, newGameRest.Tags);
            }

            res.Tags = await GameInfoGenreTagServiceInstance.GetGameTagsAsync(res.ID);

            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> EditAsync([FromBody] GameInfoRestBasic gameToUpdateRest)
        {
            var original = await GameInfoServiceInstance.GetByIDAsync(gameToUpdateRest.ID);

            if (original != null)
            {
                var gameToUpdate = Mapper.Map<GameInfo>(gameToUpdateRest);

                if (gameToUpdate.Name == null)
                {
                    gameToUpdate.Name = original.Name;
                }
                else
                {
                    var nameValidation = ValidateName(gameToUpdate);

                    if (nameValidation != null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, nameValidation.ErrorMessage);

                    }
                }

                if (gameToUpdate.Description == null)
                {
                    gameToUpdate.Description = original.Description;
                }

                if (gameToUpdate.ReleaseDate == default)
                {
                    gameToUpdate.ReleaseDate = original.ReleaseDate;
                }

                gameToUpdate.DateCreated = original.DateCreated;

                if (gameToUpdateRest.Tags != null)
                {
                    await UpdateTagsAsync(original.ID, gameToUpdateRest.Tags);
                }

                var res = Mapper.Map<GameInfoRestBasic>(await GameInfoServiceInstance.UpdateAsync(gameToUpdate));
                res.Tags = await GameInfoGenreTagServiceInstance.GetGameTagsAsync(res.ID);

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid object or incorrect id given to update action ");
        }

        [HttpDelete]
        [Route("delete/{gameID}")]
        public async Task<HttpResponseMessage> DeleteAsync(int gameID)
        {
            var result = await GameInfoServiceInstance.DeleteAsync(gameID);

            if (!result)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entry with the requested id exists in the database");
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("updateTags/{gameID}")]
        public async Task<HttpResponseMessage> UpdateTagsAsync(int gameID, [FromBody] List<string> tags)
        {
            var existingTags = await GameInfoGenreTagServiceInstance.GetGameTagsAsync(gameID);
            List<int> newTags = new List<int>();

            if (tags == null)
            {
                tags = new List<string>();
            }

            // Make sure all requested tags actually exist as genre tags in the database
            // and fill the list of new tags to create relations for
            foreach (string tagName in tags) 
            {
                var genreTag = await GameInfoGenreTagServiceInstance.GetGenreTagService().GetByNameAsync(tagName);

                if (genreTag == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The following tag wasn't found in the database: " + tagName);
                }
                else if (!existingTags.Contains(tagName))
                {
                    newTags.Add(genreTag.ID);
                }
            }

            // Remove the tag relations that exist but aren't in the new tag list
            foreach (string tag in existingTags.Except(tags))
            {
                int tagID = (await GameInfoGenreTagServiceInstance.GetGenreTagService().GetByNameAsync(tag)).ID;
                int relationID = (await GameInfoGenreTagServiceInstance.GetByGameIDAsync(gameID)).Where(x => x.GenreTagID == tagID).FirstOrDefault().ID;

                await GameInfoGenreTagServiceInstance.DeleteAsync(relationID);
            }

            // Create the new, non-existing relations
            foreach (int tagID in newTags)
            {
                GameInfoGenreTag newTag = new GameInfoGenreTag();

                newTag.GameInfoID = gameID;
                newTag.GenreTagID = tagID;

                await GameInfoGenreTagServiceInstance.InsertAsync(newTag);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task FillGameTags(GameInfoRestBasic game)
        {
            game.Tags = await GameInfoGenreTagServiceInstance.GetGameTagsAsync(game.ID);
        }

        public ValidationResult ValidateName(GameInfo gameInfo)
        {
            ValidationContext context = new ValidationContext(gameInfo, null, null) { MemberName = "Name" };
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (Validator.TryValidateProperty(gameInfo.Name, context, validationResults))
            {
                return null;
            }
            else
            {
                string str = "";

                foreach(var vr in validationResults)
                {
                    str += vr.ErrorMessage;
                    str += Environment.NewLine;
                }

                return new ValidationResult(str);
            }
        }

    }
}