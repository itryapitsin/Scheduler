namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSelectedWeekTypeIdFromUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "SelectedWeekTypeId", "dbo.WeekTypes");
            DropIndex("dbo.Users", new[] { "SelectedWeekTypeId" });
            DropColumn("dbo.Users", "SelectedWeekTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "SelectedWeekTypeId", c => c.Int());
            CreateIndex("dbo.Users", "SelectedWeekTypeId");
            AddForeignKey("dbo.Users", "SelectedWeekTypeId", "dbo.WeekTypes", "Id");
        }
    }
}
