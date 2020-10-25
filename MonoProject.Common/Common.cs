using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoProject.Common
{
    public class Common
    {
        public const int gameInfoMaxNameLength = 50;
        public const int genreTagMaxNameLength = 20;

        public static void FillEmptyParameters(ref PagingParameterModel pagingParameterModel, ref SortingParameterModel sortingParameterModel, ref SearchParameters searchParameters)
        {
            if (pagingParameterModel == null)
            {
                pagingParameterModel = new PagingParameterModel();
            }

            if (sortingParameterModel == null)
            {
                sortingParameterModel = new SortingParameterModel();
            }

            if (searchParameters == null)
            {
                searchParameters = new SearchParameters();
            }



            if (searchParameters.NameQuery == null)
            {
                searchParameters.NameQuery = "";
            }

            if (searchParameters.TagsQuery == null)
            {
                searchParameters.TagsQuery = "";
            }
        }
    }
}