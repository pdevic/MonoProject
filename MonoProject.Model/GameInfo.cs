using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class GameInfo : IPoco, IGameInfo
    {
        [ScaffoldColumn(false)] public int ID { get; set; }

        [Required] public DateTime DateCreated { get; set; }
        [Required] public DateTime DateUpdated { get; set; }
        public DateTime TimeStamp { get; set; }

        [Required, StringLength(50), Display(Name = "Game name")] public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

        //public virtual ICollection<IPlayerCountTag> PlayerCountTags { get; set; }
    }
}
