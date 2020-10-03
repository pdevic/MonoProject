using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoProject.Common
{
    public class PagingParameterModel
    {
        public const int MaxPageSize = 20;
        private int InternalPageSize { get; set; } = 5;

        public int PageSize
        {
            get { return InternalPageSize; }

            set
            {
                InternalPageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        public int PageNumber { get; set; }

    }
}
