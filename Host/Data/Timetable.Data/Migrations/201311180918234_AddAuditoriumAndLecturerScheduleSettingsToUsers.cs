namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditoriumAndLecturerScheduleSettingsToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SelectedAuditoriumId", c => c.Int());
            AddColumn("dbo.Users", "SelectedLecturerId", c => c.Int());
            AddForeignKey("dbo.Users", "SelectedAuditoriumId", "dbo.Auditoriums", "Id");
            AddForeignKey("dbo.Users", "SelectedLecturerId", "dbo.Lecturers", "Id");
            CreateIndex("dbo.Users", "SelectedAuditoriumId");
            CreateIndex("dbo.Users", "SelectedLecturerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "SelectedLecturerId" });
            DropIndex("dbo.Users", new[] { "SelectedAuditoriumId" });
            DropForeignKey("dbo.Users", "SelectedLecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.Users", "SelectedAuditoriumId", "dbo.Auditoriums");
            DropColumn("dbo.Users", "SelectedLecturerId");
            DropColumn("dbo.Users", "SelectedAuditoriumId");
        }
    }
}
