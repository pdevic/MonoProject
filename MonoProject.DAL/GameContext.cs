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
        public DbSet<GenreTagEntity> GenreTagEntities { get; set; }

        public DbSet<GameInfoGenreTagEntity> GameInfoGenreTagEntities { get; set; }

    }

}