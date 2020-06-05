namespace TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovieRequireds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "AddedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "AddedDate", c => c.DateTime(nullable: false));
        }
    }
}
