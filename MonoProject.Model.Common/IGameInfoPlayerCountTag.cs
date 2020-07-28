using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MonoProject.Model.Common
{
    public interface IGameInfoPlayerCountTag : IPoco
    {
        //[Required]
        int GameInfoID { get; set; }

        //[Required]
        int PlayerCountTagID { get; set; }

        //IGameInfo GameInfo { get; set; }

        //IPlayerCountTag PlayerCountTag { get; set; }
    }
}
