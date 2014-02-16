namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShortNameToBranches2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Branches", "ShortName");
            AddColumn("dbo.Branches", "ShortName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "ShortName");
            AddColumn("dbo.Branches", "ShortName", c => c.String());
        }
    }
}
