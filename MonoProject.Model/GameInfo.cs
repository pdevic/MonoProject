using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class GameInfo : Poco, IGameInfo
    {
        [ScaffoldColumn(false)] public int ID { get; set; }

        [Required, StringLength(50), Display(Name = "Game name")] public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<IGameInfoPlayerCountTag> GameInfoPlayerCountTags { get; set; }
    }
}
