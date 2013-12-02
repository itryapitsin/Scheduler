namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettingFieldsToUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            Sql(@"
INSERT INTO [dbo].[Semesters]
           ([Name]
           ,[IsActual]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           ('Первый семестр'
           ,1
           ,getdate()
           ,getdate())");

            Sql(@"
INSERT INTO [dbo].[Semesters]
           ([Name]
           ,[IsActual]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           ('Второй семестр'
           ,1
           ,getdate()
           ,getdate())");

            Sql(@"
INSERT INTO [dbo].[Semesters]
           ([Name]
           ,[IsActual]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           ('Третий семестр'
           ,1
           ,getdate()
           ,getdate())");

            Sql(@"
INSERT INTO [dbo].[Semesters]
           ([Name]
           ,[IsActual]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           ('Четвертый семестр'
           ,1
           ,getdate()
           ,getdate())");

            
            
            CreateTable(
                "dbo.GroupsToUsers",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Group_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Group_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Group_Id);
            
            AddColumn("dbo.Users", "SelectedBranchhId", c => c.Int());
            AddColumn("dbo.Users", "SelectedFacultyId", c => c.Int());
            AddColumn("dbo.Users", "SelectedCourseId", c => c.Int());
            AddColumn("dbo.Users", "SelectedBuildingId", c => c.Int());
            AddColumn("dbo.Users", "SelectedStudyYearId", c => c.Int());
            AddColumn("dbo.Users", "SelectedSemesterId", c => c.Int());
            AddColumn("dbo.Users", "SelectedWeekTypeId", c => c.Int());
            AddColumn("dbo.Users", "SelectedBranch_Id", c => c.Int());
            AddColumn("dbo.ScheduleInfoes", "SemesterId", c => c.Int(nullable: false));

            Sql(@"
UPDATE [dbo].[ScheduleInfoes]
    SET [Semester] = 3
    WHERE [Semester] > 2
            ");

            Sql(@"
UPDATE [dbo].[ScheduleInfoes]
    SET [SemesterId] = [Semester] + 1
            ");

            AddForeignKey("dbo.Users", "SelectedBranch_Id", "dbo.Branches", "Id");
            AddForeignKey("dbo.Users", "SelectedFacultyId", "dbo.Faculties", "Id");
            AddForeignKey("dbo.Users", "SelectedCourseId", "dbo.Courses", "Id");
            AddForeignKey("dbo.Users", "SelectedBuildingId", "dbo.Buildings", "Id");
            AddForeignKey("dbo.Users", "SelectedStudyYearId", "dbo.StudyYears", "Id");
            AddForeignKey("dbo.Users", "SelectedSemesterId", "dbo.Semesters", "Id");
            AddForeignKey("dbo.Users", "SelectedWeekTypeId", "dbo.WeekTypes", "Id");
            AddForeignKey("dbo.ScheduleInfoes", "SemesterId", "dbo.Semesters", "Id", cascadeDelete: true);
            CreateIndex("dbo.Users", "SelectedBranch_Id");
            CreateIndex("dbo.Users", "SelectedFacultyId");
            CreateIndex("dbo.Users", "SelectedCourseId");
            CreateIndex("dbo.Users", "SelectedBuildingId");
            CreateIndex("dbo.Users", "SelectedStudyYearId");
            CreateIndex("dbo.Users", "SelectedSemesterId");
            CreateIndex("dbo.Users", "SelectedWeekTypeId");
            CreateIndex("dbo.ScheduleInfoes", "SemesterId");
            //DropColumn("dbo.ScheduleInfoes", "Semester");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.ScheduleInfoes", "Semester", c => c.Int(nullable: false));
            DropIndex("dbo.GroupsToUsers", new[] { "Group_Id" });
            DropIndex("dbo.GroupsToUsers", new[] { "User_Id" });
            DropIndex("dbo.ScheduleInfoes", new[] { "SemesterId" });
            DropIndex("dbo.Users", new[] { "SelectedWeekTypeId" });
            DropIndex("dbo.Users", new[] { "SelectedSemesterId" });
            DropIndex("dbo.Users", new[] { "SelectedStudyYearId" });
            DropIndex("dbo.Users", new[] { "SelectedBuildingId" });
            DropIndex("dbo.Users", new[] { "SelectedCourseId" });
            DropIndex("dbo.Users", new[] { "SelectedFacultyId" });
            DropIndex("dbo.Users", new[] { "SelectedBranch_Id" });
            DropForeignKey("dbo.GroupsToUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.GroupsToUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ScheduleInfoes", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Users", "SelectedWeekTypeId", "dbo.WeekTypes");
            DropForeignKey("dbo.Users", "SelectedSemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Users", "SelectedStudyYearId", "dbo.StudyYears");
            DropForeignKey("dbo.Users", "SelectedBuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Users", "SelectedCourseId", "dbo.Courses");
            DropForeignKey("dbo.Users", "SelectedFacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Users", "SelectedBranch_Id", "dbo.Branches");
            DropColumn("dbo.ScheduleInfoes", "SemesterId");
            DropColumn("dbo.Users", "SelectedBranch_Id");
            DropColumn("dbo.Users", "SelectedWeekTypeId");
            DropColumn("dbo.Users", "SelectedSemesterId");
            DropColumn("dbo.Users", "SelectedStudyYearId");
            DropColumn("dbo.Users", "SelectedBuildingId");
            DropColumn("dbo.Users", "SelectedCourseId");
            DropColumn("dbo.Users", "SelectedFacultyId");
            DropColumn("dbo.Users", "SelectedBranchhId");
            DropTable("dbo.GroupsToUsers");
            DropTable("dbo.Semesters");
        }
    }
}
