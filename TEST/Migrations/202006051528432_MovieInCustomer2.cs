namespace TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieInCustomer2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customers", name: "Movie_Id", newName: "MovieId");
            RenameIndex(table: "dbo.Customers", name: "IX_Movie_Id", newName: "IX_MovieId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Customers", name: "IX_MovieId", newName: "IX_Movie_Id");
            RenameColumn(table: "dbo.Customers", name: "MovieId", newName: "Movie_Id");
        }
    }
}
