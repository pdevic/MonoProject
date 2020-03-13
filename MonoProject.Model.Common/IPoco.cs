using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MonoProject.Model.Common
{
    public interface IPoco
    {
        [ScaffoldColumn(false)] int ID { get; set; }

        [Required] DateTime DateCreated { get; set; }
        [Required] DateTime DateUpdated { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
