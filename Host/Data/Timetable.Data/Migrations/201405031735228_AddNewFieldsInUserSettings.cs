namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFieldsInUserSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AuditoriumScheduleSelectedTimeId", c => c.Int());
            AddColumn("dbo.Users", "AuditoriumScheduleSelectedDayOfWeek", c => c.Int());
            AddForeignKey("dbo.Users", "AuditoriumScheduleSelectedTimeId", "dbo.Times", "Id");
            CreateIndex("dbo.Users", "AuditoriumScheduleSelectedTimeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "AuditoriumScheduleSelectedTimeId" });
            DropForeignKey("dbo.Users", "AuditoriumScheduleSelectedTimeId", "dbo.Times");
            DropColumn("dbo.Users", "AuditoriumScheduleSelectedDayOfWeek");
            DropColumn("dbo.Users", "AuditoriumScheduleSelectedTimeId");
        }
    }
}
