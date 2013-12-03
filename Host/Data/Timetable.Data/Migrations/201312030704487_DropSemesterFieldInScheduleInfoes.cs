namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropSemesterFieldInScheduleInfoes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ScheduleInfoes", "Semester");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScheduleInfoes", "Semester", builder => builder.Int(false));
        }
    }
}
