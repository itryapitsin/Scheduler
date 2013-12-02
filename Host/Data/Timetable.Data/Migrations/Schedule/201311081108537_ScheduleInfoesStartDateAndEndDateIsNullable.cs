namespace Timetable.Data.Migrations.Schedule
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleInfoesStartDateAndEndDateIsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ScheduleInfoes", "StartDate", c => c.DateTime());
            AlterColumn("dbo.ScheduleInfoes", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ScheduleInfoes", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ScheduleInfoes", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
