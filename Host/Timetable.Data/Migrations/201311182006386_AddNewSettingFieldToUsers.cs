namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewSettingFieldToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AuditoriumScheduleSelectedBuildingId", c => c.Int());
            AddForeignKey("dbo.Users", "AuditoriumScheduleSelectedBuildingId", "dbo.Buildings", "Id");
            CreateIndex("dbo.Users", "AuditoriumScheduleSelectedBuildingId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "AuditoriumScheduleSelectedBuildingId" });
            DropForeignKey("dbo.Users", "AuditoriumScheduleSelectedBuildingId", "dbo.Buildings");
            DropColumn("dbo.Users", "AuditoriumScheduleSelectedBuildingId");
        }
    }
}
