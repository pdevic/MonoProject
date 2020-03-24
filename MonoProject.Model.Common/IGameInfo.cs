﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MonoProject.Model.Common
{
    public interface IGameInfo : IPoco
    {
        [Required, StringLength(MonoProject.Common.Common.gameInfoNameLength), Display(Name = "Game name")] string Name { get; set; }
        string Description { get; set; }
        DateTime ReleaseDate { get; set; }

        ICollection<IGameInfoPlayerCountTag> GameInfoPlayerCountTags { get; set; }
    }
}
