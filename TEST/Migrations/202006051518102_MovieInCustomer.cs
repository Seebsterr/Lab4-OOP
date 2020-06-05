namespace TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieInCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Movie_Id", c => c.Int());
            CreateIndex("dbo.Customers", "Movie_Id");
            AddForeignKey("dbo.Customers", "Movie_Id", "dbo.Movies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.Customers", new[] { "Movie_Id" });
            DropColumn("dbo.Customers", "Movie_Id");
        }
    }
}
