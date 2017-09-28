namespace WeatherNote.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateNotes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Notes (Message, Date) VALUES ('Wash the car', 2017-9-28)");
            Sql("INSERT INTO Notes (Message, Date) VALUES ('Walk the dog', 2017-9-29)");
            Sql("INSERT INTO Notes (Message, Date) VALUES ('Spy on neighbour', 2017-9-30)");

        }

        public override void Down()
        {
        }
    }
}
