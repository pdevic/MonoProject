using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MonoProject.Model.Common
{
    public interface IGenreTag : IPoco
    {
        [Required, StringLength(30)] string Name { get; set; }

        //ICollection<IGameInfo> GameInfoes { get; set; }
    }
}
