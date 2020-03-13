using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class PlayerCountTag : Poco, IPlayerCountTag
    {
        [ScaffoldColumn(false)] public int ID { get; set; }
        [Required, StringLength(30)] public string Name { get; set; }
    }
}

