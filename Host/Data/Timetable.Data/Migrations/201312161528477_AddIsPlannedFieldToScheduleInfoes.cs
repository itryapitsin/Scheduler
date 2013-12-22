namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsPlannedFieldToScheduleInfoes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleInfoes", "IsPlanned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleInfoes", "IsPlanned");
        }
    }
}
