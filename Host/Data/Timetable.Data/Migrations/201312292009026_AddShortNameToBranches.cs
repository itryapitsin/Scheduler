namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShortNameToBranches : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "ShortName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "ShortName");
        }
    }
}
