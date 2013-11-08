namespace Timetable.Data.Migrations.Schedule
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleInfoesHoursPerWeekChangeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ScheduleInfoes", "HoursPerWeek", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ScheduleInfoes", "HoursPerWeek", c => c.Int(nullable: false));
        }
    }
}
