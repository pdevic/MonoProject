namespace MonoProject.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using MonoProject.Model;

    public class GameContext : DbContext
    {
        public GameContext()
            : base("GameContext")
        {
            
        }

        public DbSet<GameInfo> GameInfos { get; set; }
        public DbSet<PlayerCountTag> PlayerCountTags { get; set; }

        public DbSet<GameInfoPlayerCountTag> GameInfoPlayerCountTags { get; set; }

    }

}