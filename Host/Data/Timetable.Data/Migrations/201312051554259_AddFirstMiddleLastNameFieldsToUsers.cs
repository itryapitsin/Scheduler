namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirstMiddleLastNameFieldsToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Firstname", c => c.String());
            AddColumn("dbo.Users", "Middlename", c => c.String());
            AddColumn("dbo.Users", "Lastname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Lastname");
            DropColumn("dbo.Users", "Middlename");
            DropColumn("dbo.Users", "Firstname");
        }
    }
}
