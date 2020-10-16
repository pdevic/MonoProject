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

        public static void FillEmptyParameters(PagingParameterModel pagingParameterModel, SortingParameterModel sortingParameterModel)
        {
            if (pagingParameterModel == null)
            {
                pagingParameterModel = new PagingParameterModel();
            }

            if (sortingParameterModel == null)
            {
                sortingParameterModel = new SortingParameterModel();
            }
        }
    }
}