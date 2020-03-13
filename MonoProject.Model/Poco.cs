using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MonoProject.Model
{
    public class Poco
    {
        [ScaffoldColumn(false)] public int ID { get; set; }

        [Required] public DateTime DateCreated { get; set; }
        [Required] public DateTime DateUpdated { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
