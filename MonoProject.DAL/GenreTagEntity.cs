using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;

namespace MonoProject.DAL
{
    public class GenreTagEntity : IPoco
    {
        [ScaffoldColumn(false)] public int ID { get; set; }

        [Required, StringLength(30)] public string Name { get; set; }

        [Required] public DateTime DateCreated { get; set; }
        [Required] public DateTime DateUpdated { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
