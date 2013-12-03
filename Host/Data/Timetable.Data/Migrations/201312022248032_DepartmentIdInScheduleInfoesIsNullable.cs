namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentIdInScheduleInfoesIsNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleInfoes", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.ScheduleInfoes", new[] { "DepartmentId" });
            AlterColumn("dbo.ScheduleInfoes", "DepartmentId", c => c.Int());
            AddForeignKey("dbo.ScheduleInfoes", "DepartmentId", "dbo.Departments", "Id");
            CreateIndex("dbo.ScheduleInfoes", "DepartmentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ScheduleInfoes", new[] { "DepartmentId" });
            DropForeignKey("dbo.ScheduleInfoes", "DepartmentId", "dbo.Departments");
            AlterColumn("dbo.ScheduleInfoes", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.ScheduleInfoes", "DepartmentId");
            AddForeignKey("dbo.ScheduleInfoes", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
