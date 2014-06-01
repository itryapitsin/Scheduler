namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditoriumOrderDateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AuditoriumScheduleSelectedDate", c => c.DateTime());
            AddColumn("dbo.AuditoriumOrders", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "AuditoriumScheduleSelectedDayOfWeek");
            DropColumn("dbo.AuditoriumOrders", "DayOfWeek");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditoriumOrders", "DayOfWeek", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "AuditoriumScheduleSelectedDayOfWeek", c => c.Int());
            DropColumn("dbo.AuditoriumOrders", "Date");
            DropColumn("dbo.Users", "AuditoriumScheduleSelectedDate");
        }
    }
}
