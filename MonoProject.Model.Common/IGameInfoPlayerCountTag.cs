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
        IGameInfo GameInfo { get; set; }
        IPlayerCountTag PlayerCountTag { get; set; }
    }
}
