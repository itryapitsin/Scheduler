namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIIASKeyToScheduleTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleTypes", "IIASKey", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleTypes", "IIASKey");
        }
    }
}
