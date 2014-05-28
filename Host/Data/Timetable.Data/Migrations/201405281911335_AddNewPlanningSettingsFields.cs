namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewPlanningSettingsFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PlanningModalSelectedScheduleTypeId", c => c.Int());
            AddColumn("dbo.Users", "PlanningModalSelectedBuildingId", c => c.Int());
            AddColumn("dbo.Users", "PlanningModalSelectedWeekTypeId", c => c.Int());
            AddColumn("dbo.Users", "PlanningModalSelectedAuditoriumId", c => c.Int());
            AddColumn("dbo.Users", "PlanningModalSelectedSubGroup", c => c.String());
            AddForeignKey("dbo.Users", "PlanningModalSelectedScheduleTypeId", "dbo.ScheduleTypes", "Id");
            AddForeignKey("dbo.Users", "PlanningModalSelectedBuildingId", "dbo.Buildings", "Id");
            AddForeignKey("dbo.Users", "PlanningModalSelectedWeekTypeId", "dbo.WeekTypes", "Id");
            AddForeignKey("dbo.Users", "PlanningModalSelectedAuditoriumId", "dbo.Auditoriums", "Id");
            CreateIndex("dbo.Users", "PlanningModalSelectedScheduleTypeId");
            CreateIndex("dbo.Users", "PlanningModalSelectedBuildingId");
            CreateIndex("dbo.Users", "PlanningModalSelectedWeekTypeId");
            CreateIndex("dbo.Users", "PlanningModalSelectedAuditoriumId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "PlanningModalSelectedAuditoriumId" });
            DropIndex("dbo.Users", new[] { "PlanningModalSelectedWeekTypeId" });
            DropIndex("dbo.Users", new[] { "PlanningModalSelectedBuildingId" });
            DropIndex("dbo.Users", new[] { "PlanningModalSelectedScheduleTypeId" });
            DropForeignKey("dbo.Users", "PlanningModalSelectedAuditoriumId", "dbo.Auditoriums");
            DropForeignKey("dbo.Users", "PlanningModalSelectedWeekTypeId", "dbo.WeekTypes");
            DropForeignKey("dbo.Users", "PlanningModalSelectedBuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Users", "PlanningModalSelectedScheduleTypeId", "dbo.ScheduleTypes");
            DropColumn("dbo.Users", "PlanningModalSelectedSubGroup");
            DropColumn("dbo.Users", "PlanningModalSelectedAuditoriumId");
            DropColumn("dbo.Users", "PlanningModalSelectedWeekTypeId");
            DropColumn("dbo.Users", "PlanningModalSelectedBuildingId");
            DropColumn("dbo.Users", "PlanningModalSelectedScheduleTypeId");
        }
    }
}
