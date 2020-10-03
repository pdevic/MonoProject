using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoProject.Common
{
    public class SortingParameterModel
    {
        public static ReadOnlyCollection<string> OrderByOptions = (new List<string>() { "Name", "ReleaseDate" }).AsReadOnly();
        public static ReadOnlyCollection<string> SortingOrderOptions = (new List<string>() { "Asc", "Desc" }).AsReadOnly();

        public string OrderBy { get; set; } = OrderByOptions.First();
        public string SortingOrder { get; set; } = SortingOrderOptions.First();
    }
}
