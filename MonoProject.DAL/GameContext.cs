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

        public DbSet<GameInfo> GameInfos { get; set; }
        public DbSet<PlayerCountTag> PlayerCountTags { get; set; }

        public DbSet<GameInfoPlayerCountTag> GameInfoPlayerCountTags { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<GameContext>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameInfo>().ToTable("GameInfo");
            modelBuilder.Entity<PlayerCountTag>().ToTable("PlayerCountTag");
            modelBuilder.Entity<GameInfoPlayerCountTag>().ToTable("GameInfoPlayerCountTag");
        }*/

    }

}