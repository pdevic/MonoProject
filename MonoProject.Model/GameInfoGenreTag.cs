﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MonoProject.Model.Common;

namespace MonoProject.Model
{
    public class GameInfoGenreTag : IPoco, IGameInfoGenreTag
    {
        [ScaffoldColumn(false)] public int ID { get; set; }

        public int GameInfoID { get; set; }
        public int GenreTagID { get; set; }

        [Required] public DateTime DateCreated { get; set; }
        [Required] public DateTime DateUpdated { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
