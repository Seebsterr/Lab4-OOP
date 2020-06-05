namespace TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GenresId = c.Byte(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        StockId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenresId, cascadeDelete: true)
                .Index(t => t.GenresId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "GenresId", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "GenresId" });
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
        }
    }
}
