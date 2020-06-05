namespace TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into GENRES (Id, Name) Values (0, 'Comedy')");
            Sql("Insert into GENRES (Id, Name) Values (1, 'Action')");
            Sql("Insert into GENRES (Id, Name) Values (2, 'Family')");
            Sql("Insert into GENRES (Id, Name) Values (3, 'Romance')");
        }

        public override void Down()
        {
        }
    }
}
