namespace MonoProject.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using MonoProject.Model;

    public class GameContext : DbContext
    {
        public GameContext()
            : base("GameDatabase")
        {
            
        }

        public DbSet<GameInfoEntity> GameInfoEntities { get; set; }
        public DbSet<PlayerCountTagEntity> PlayerCountTagEntities { get; set; }

        public DbSet<GameInfoPlayerCountTagEntity> GameInfoPlayerCountTagEntities { get; set; }

    }

}