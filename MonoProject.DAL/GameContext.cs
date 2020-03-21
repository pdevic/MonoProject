namespace MonoProject.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using MonoProject.Model.Common;

    public class GameContext : DbContext
    {
        public GameContext()
            : base("name=GameContext")
        {
        }

        public DbSet<IGameInfo> GameInfos { get; set; }
        public DbSet<IPlayerCountTag> PlayerCountTags { get; set; }

        public DbSet<IGameInfoPlayerCountTag> GameInfoPlayerCountTags { get; set; }

    }

}