using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class GameInfoPlayerCountTag : Poco, IGameInfoPlayerCountTag
    {
        [ScaffoldColumn(false)] public int ID { get; set; }

        public virtual IGameInfo GameInfo { get; set; }
        public virtual IPlayerCountTag PlayerCountTag { get; set; }
    }
}
