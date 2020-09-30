using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model.Common;

namespace MonoProject.DAL
{
    public class GameInfoPlayerCountTagEntity : IPoco
    {
        [ScaffoldColumn(false)] public int ID { get; set; }

        public int GameInfoID { get; set; }
        public int PlayerCountTagID { get; set; }

        [Required] public DateTime DateCreated { get; set; }
        [Required] public DateTime DateUpdated { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
