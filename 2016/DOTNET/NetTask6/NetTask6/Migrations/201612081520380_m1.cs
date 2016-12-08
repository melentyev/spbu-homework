namespace NetTask6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ActorId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        Image = c.String(),
                        Director_DirectorId = c.Int(),
                    })
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.Directors", t => t.Director_DirectorId)
                .Index(t => t.Director_DirectorId);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        DirectorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DirectorId);
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Movie_MovieId = c.Int(nullable: false),
                        Actor_ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_MovieId, t.Actor_ActorId })
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_ActorId, cascadeDelete: true)
                .Index(t => t.Movie_MovieId)
                .Index(t => t.Actor_ActorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Director_DirectorId", "dbo.Directors");
            DropForeignKey("dbo.MovieActors", "Actor_ActorId", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "Movie_MovieId", "dbo.Movies");
            DropIndex("dbo.MovieActors", new[] { "Actor_ActorId" });
            DropIndex("dbo.MovieActors", new[] { "Movie_MovieId" });
            DropIndex("dbo.Movies", new[] { "Director_DirectorId" });
            DropTable("dbo.MovieActors");
            DropTable("dbo.Directors");
            DropTable("dbo.Movies");
            DropTable("dbo.Actors");
        }
    }
}
