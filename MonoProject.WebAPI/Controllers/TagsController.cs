using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using AutoMapper;

using MonoProject.Common;
using MonoProject.Model;
using MonoProject.Service.Common;

using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace MonoProject.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/tags")]
    public class TagsController : ApiController
    {
        private readonly IGenreTagService GenreTagServiceInstance;
        private readonly IMapper Mapper;

        public class GenreTagRestBasic
        {
            [Required] public int ID { get; set; }
            [Required] public string Name { get; set; }
        }

        public TagsController(IGenreTagService genreTagService, IMapper mapper)
        {
            GenreTagServiceInstance = genreTagService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("index")]
        public async Task<HttpResponseMessage> IndexAsync([FromUri] PagingParameterModel pagingParameterModel, [FromUri] SortingParameterModel sortingParameterModel, [FromUri] SearchParameters searchParameters)
        {
            Common.Common.FillEmptyParameters(pagingParameterModel, sortingParameterModel, searchParameters);

            if (sortingParameterModel.OrderBy != "Name")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usupported sorting parameter \"" + sortingParameterModel.OrderBy + "\" for tags");
            }

            if (!SortingParameterModel.SortingOrderOptions.Contains(sortingParameterModel.SortingOrder))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unknown sorting order parameter " + sortingParameterModel.SortingOrder);
            }

            var res = Mapper.Map<List<GenreTagRestBasic>>(await GenreTagServiceInstance.GetListAsync(pagingParameterModel, sortingParameterModel, searchParameters));
            var totalItemsCount = await GenreTagServiceInstance.GetAllCountAsync();

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
        [Route("get/{tagID}")]
        public async Task<HttpResponseMessage> GetAsync(int tagID)
        {
            var entity = await GenreTagServiceInstance.GetByIDAsync(tagID);

            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Entry with requested id doesn't exist in the database");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<GenreTagRestBasic>(entity));
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync(GenreTagRestBasic newGameRest)
        {
            var newTag = Mapper.Map<GenreTag>(newGameRest);
            var nameValidation = ValidateName(newTag);

            if (nameValidation != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, nameValidation.ErrorMessage);
            }

            if (await GenreTagServiceInstance.GetByNameAsync(newTag.Name) != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "A request was made to create a new tag that would be a duplicate of \'" + newTag.Name + "\'");
            }

            var res = Mapper.Map<GenreTagRestBasic>(await GenreTagServiceInstance.InsertAsync(newTag));
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> EditAsync(GenreTagRestBasic tagToUpdateRest)
        {
            var original = await GenreTagServiceInstance.GetByIDAsync(tagToUpdateRest.ID);

            if (original != null)
            {
                var tagToUpdate = Mapper.Map<GenreTag>(tagToUpdateRest);

                if (tagToUpdate.Name == null)
                {
                    tagToUpdate.Name = original.Name;
                }
                else
                {
                    var nameValidation = ValidateName(tagToUpdate);

                    if (nameValidation != null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, nameValidation.ErrorMessage);

                    }
                }

                tagToUpdate.DateCreated = original.DateCreated;

                var res = Mapper.Map<GenreTagRestBasic>(await GenreTagServiceInstance.UpdateAsync(tagToUpdate));
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid object or incorrect id given to update action ");
        }

        [HttpDelete]
        [Route("delete/{gameID}")]
        public async Task<HttpResponseMessage> DeleteAsync(int gameID)
        {
            var result = await GenreTagServiceInstance.DeleteAsync(gameID);

            if (!result)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entry with the requested id exists in the database");
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        ValidationResult ValidateName(GenreTag tag)
        {
            ValidationContext context = new ValidationContext(tag, null, null) { MemberName = "Name" };
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (Validator.TryValidateProperty(tag.Name, context, validationResults))
            {
                return null;
            }
            else
            {
                string str = "";

                foreach (var vr in validationResults)
                {
                    str += vr.ErrorMessage;
                    str += Environment.NewLine;
                }

                return new ValidationResult(str);
            }
        }

    }
}
