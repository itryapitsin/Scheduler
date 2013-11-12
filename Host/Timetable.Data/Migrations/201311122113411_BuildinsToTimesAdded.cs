namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuildinsToTimesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuildingsToTimes",
                c => new
                    {
                        Time_Id = c.Int(nullable: false),
                        Building_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Time_Id, t.Building_Id })
                .ForeignKey("dbo.Times", t => t.Time_Id, cascadeDelete: true)
                .ForeignKey("dbo.Buildings", t => t.Building_Id, cascadeDelete: true)
                .Index(t => t.Time_Id)
                .Index(t => t.Building_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BuildingsToTimes", new[] { "Building_Id" });
            DropIndex("dbo.BuildingsToTimes", new[] { "Time_Id" });
            DropForeignKey("dbo.BuildingsToTimes", "Building_Id", "dbo.Buildings");
            DropForeignKey("dbo.BuildingsToTimes", "Time_Id", "dbo.Times");
            DropTable("dbo.BuildingsToTimes");
        }
    }
}
