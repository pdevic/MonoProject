using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MonoProject.Model.Common
{
    public interface IPlayerCountTag : IPoco
    {
        [Required, StringLength(30)] string Name { get; set; }
    }
}
