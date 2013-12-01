namespace Timetable.Data.Migrations.Schedule
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBuildingFromTimes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Times", "Building_Id", "dbo.Buildings");
            DropIndex("dbo.Times", new[] { "Building_Id" });
            DropColumn("dbo.Times", "BuildingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Times", "BuildingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Times", "BuildingId");
            AddForeignKey("dbo.Times", "BuildingId", "dbo.Buildings", "Id");
        }
    }
}
