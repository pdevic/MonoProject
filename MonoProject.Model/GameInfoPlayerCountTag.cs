using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class GameInfoPlayerCountTag : IPoco, IGameInfoPlayerCountTag
    {
        [ScaffoldColumn(false)] public int ID { get; set; }

        [Required] public DateTime DateCreated { get; set; }
        [Required] public DateTime DateUpdated { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual IGameInfo GameInfo { get; set; }
        public virtual IPlayerCountTag PlayerCountTag { get; set; }
    }
}
