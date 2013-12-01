namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePeriodToTime : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Schedules", "PeriodId", "dbo.Times");
            //DropIndex("dbo.Schedules", new[] { "PeriodId" });
            //AddColumn("dbo.Schedules", "TimeId", c => c.Int(nullable: false));
            //AddForeignKey("dbo.Schedules", "TimeId", "dbo.Times", "Id", cascadeDelete: true);
            //CreateIndex("dbo.Schedules", "TimeId");
            //DropColumn("dbo.Schedules", "PeriodId");
            RenameColumn("dbo.Schedules", "PeriodId", "TimeId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Schedules", "PeriodId", c => c.Int(nullable: false));
            //DropIndex("dbo.Schedules", new[] { "TimeId" });
            //DropForeignKey("dbo.Schedules", "TimeId", "dbo.Times");
            //DropColumn("dbo.Schedules", "TimeId");
            //CreateIndex("dbo.Schedules", "PeriodId");
            //AddForeignKey("dbo.Schedules", "PeriodId", "dbo.Times", "Id", cascadeDelete: true);
            RenameColumn("dbo.Schedules", "TimeId", "PeriodId");
        }
    }
}
