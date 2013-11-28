namespace Timetable.Data.Migrations.Schedule
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IIASKeyAddedToEveryoneTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "IIASKey", c => c.Long());
            AddColumn("dbo.Faculties", "IIASKey", c => c.Long());
            AddColumn("dbo.ScheduleInfoes", "IIASKey", c => c.Long());
            AddColumn("dbo.Courses", "IIASKey", c => c.Long());
            AddColumn("dbo.Specialities", "IIASKey", c => c.Long());
            AddColumn("dbo.Groups", "IIASKey", c => c.Long());
            AddColumn("dbo.Auditoriums", "IIASKey", c => c.Long());
            AddColumn("dbo.TutorialTypes", "IIASKey", c => c.Long());
            AddColumn("dbo.Buildings", "IIASKey", c => c.Long());
            AddColumn("dbo.AuditoriumTypes", "IIASKey", c => c.Long());
            AddColumn("dbo.Schedules", "IIASKey", c => c.Long());
            AddColumn("dbo.Times", "IIASKey", c => c.Long());
            AddColumn("dbo.WeekTypes", "IIASKey", c => c.Long());
            AddColumn("dbo.TimetableEntities", "IIASKey", c => c.Long());
            AddColumn("dbo.Lecturers", "IIASKey", c => c.Long());
            AddColumn("dbo.Positions", "IIASKey", c => c.Long());
            AddColumn("dbo.Tutorials", "IIASKey", c => c.Long());
            AddColumn("dbo.StudyYears", "IIASKey", c => c.Long());
            AddColumn("dbo.Branches", "IIASKey", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "IIASKey");
            DropColumn("dbo.StudyYears", "IIASKey");
            DropColumn("dbo.Tutorials", "IIASKey");
            DropColumn("dbo.Positions", "IIASKey");
            DropColumn("dbo.Lecturers", "IIASKey");
            DropColumn("dbo.TimetableEntities", "IIASKey");
            DropColumn("dbo.WeekTypes", "IIASKey");
            DropColumn("dbo.Times", "IIASKey");
            DropColumn("dbo.Schedules", "IIASKey");
            DropColumn("dbo.AuditoriumTypes", "IIASKey");
            DropColumn("dbo.Buildings", "IIASKey");
            DropColumn("dbo.TutorialTypes", "IIASKey");
            DropColumn("dbo.Auditoriums", "IIASKey");
            DropColumn("dbo.Groups", "IIASKey");
            DropColumn("dbo.Specialities", "IIASKey");
            DropColumn("dbo.Courses", "IIASKey");
            DropColumn("dbo.ScheduleInfoes", "IIASKey");
            DropColumn("dbo.Faculties", "IIASKey");
            DropColumn("dbo.Departments", "IIASKey");
        }
    }
}
