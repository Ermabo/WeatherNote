namespace WeatherNote.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedToDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "Date", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "Date", c => c.DateTime(nullable: false));
        }
    }
}
