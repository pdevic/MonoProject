using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class PlayerCountTag : IPoco, IPlayerCountTag
    {
        [ScaffoldColumn(false)] public int ID { get; set; }

        [Required] public DateTime DateCreated { get; set; }
        [Required] public DateTime DateUpdated { get; set; }
        public DateTime TimeStamp { get; set; }

        [Required, StringLength(30)] public string Name { get; set; }

        //public virtual ICollection<IGameInfo> GameInfoes { get; set; }
    }
}

