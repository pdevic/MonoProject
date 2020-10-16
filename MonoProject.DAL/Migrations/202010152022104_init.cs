namespace MonoProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameInfoGenreTagEntities", "GenreTagID", c => c.Int(nullable: false));
            DropColumn("dbo.GameInfoGenreTagEntities", "PlayerCountTagID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameInfoGenreTagEntities", "PlayerCountTagID", c => c.Int(nullable: false));
            DropColumn("dbo.GameInfoGenreTagEntities", "GenreTagID");
        }
    }
}
