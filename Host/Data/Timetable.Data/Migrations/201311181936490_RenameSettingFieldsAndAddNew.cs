namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSettingFieldsAndAddNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "SelectedBranch_Id", "dbo.Branches");
            DropForeignKey("dbo.Users", "SelectedFacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Users", "SelectedCourseId", "dbo.Courses");
            DropForeignKey("dbo.Users", "SelectedBuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Users", "SelectedStudyYearId", "dbo.StudyYears");
            DropForeignKey("dbo.Users", "SelectedSemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Users", "SelectedAuditoriumId", "dbo.Auditoriums");
            DropForeignKey("dbo.Users", "SelectedLecturerId", "dbo.Lecturers");
            DropIndex("dbo.Users", new[] { "SelectedBranch_Id" });
            DropIndex("dbo.Users", new[] { "SelectedFacultyId" });
            DropIndex("dbo.Users", new[] { "SelectedCourseId" });
            DropIndex("dbo.Users", new[] { "SelectedBuildingId" });
            DropIndex("dbo.Users", new[] { "SelectedStudyYearId" });
            DropIndex("dbo.Users", new[] { "SelectedSemesterId" });
            DropIndex("dbo.Users", new[] { "SelectedAuditoriumId" });
            DropIndex("dbo.Users", new[] { "SelectedLecturerId" });
            AddColumn("dbo.Users", "CreatorSelectedBranchId", c => c.Int());
            AddColumn("dbo.Users", "CreatorSelectedFacultyId", c => c.Int());
            AddColumn("dbo.Users", "CreatorSelectedCourseId", c => c.Int());
            AddColumn("dbo.Users", "CreatorSelectedBuildingId", c => c.Int());
            AddColumn("dbo.Users", "CreatorSelectedStudyYearId", c => c.Int());
            AddColumn("dbo.Users", "CreatorSelectedSemesterId", c => c.Int());
            AddColumn("dbo.Users", "CreatorSelectedAuditoriumId", c => c.Int());
            AddColumn("dbo.Users", "AuditoriumScheduleSelectedAuditoriumId", c => c.Int());
            AddColumn("dbo.Users", "AuditoriumScheduleSelectedStudyYearId", c => c.Int());
            AddColumn("dbo.Users", "AuditoriumScheduleSelectedSemesterId", c => c.Int());
            AddColumn("dbo.Users", "LecturerScheduleSelectedLecturerId", c => c.Int());
            AddColumn("dbo.Users", "LecturerScheduleSelectedStudyYearId", c => c.Int());
            AddColumn("dbo.Users", "LecturerScheduleSelectedSemesterId", c => c.Int());
            AddForeignKey("dbo.Users", "CreatorSelectedBranchId", "dbo.Branches", "Id");
            AddForeignKey("dbo.Users", "CreatorSelectedFacultyId", "dbo.Faculties", "Id");
            AddForeignKey("dbo.Users", "CreatorSelectedCourseId", "dbo.Courses", "Id");
            AddForeignKey("dbo.Users", "CreatorSelectedBuildingId", "dbo.Buildings", "Id");
            AddForeignKey("dbo.Users", "CreatorSelectedStudyYearId", "dbo.StudyYears", "Id");
            AddForeignKey("dbo.Users", "CreatorSelectedSemesterId", "dbo.Semesters", "Id");
            AddForeignKey("dbo.Users", "CreatorSelectedAuditoriumId", "dbo.Auditoriums", "Id");
            AddForeignKey("dbo.Users", "AuditoriumScheduleSelectedAuditoriumId", "dbo.Auditoriums", "Id");
            AddForeignKey("dbo.Users", "AuditoriumScheduleSelectedStudyYearId", "dbo.StudyYears", "Id");
            AddForeignKey("dbo.Users", "AuditoriumScheduleSelectedSemesterId", "dbo.Semesters", "Id");
            AddForeignKey("dbo.Users", "LecturerScheduleSelectedLecturerId", "dbo.Lecturers", "Id");
            AddForeignKey("dbo.Users", "LecturerScheduleSelectedStudyYearId", "dbo.StudyYears", "Id");
            AddForeignKey("dbo.Users", "LecturerScheduleSelectedSemesterId", "dbo.Semesters", "Id");
            CreateIndex("dbo.Users", "CreatorSelectedBranchId");
            CreateIndex("dbo.Users", "CreatorSelectedFacultyId");
            CreateIndex("dbo.Users", "CreatorSelectedCourseId");
            CreateIndex("dbo.Users", "CreatorSelectedBuildingId");
            CreateIndex("dbo.Users", "CreatorSelectedStudyYearId");
            CreateIndex("dbo.Users", "CreatorSelectedSemesterId");
            CreateIndex("dbo.Users", "CreatorSelectedAuditoriumId");
            CreateIndex("dbo.Users", "AuditoriumScheduleSelectedAuditoriumId");
            CreateIndex("dbo.Users", "AuditoriumScheduleSelectedStudyYearId");
            CreateIndex("dbo.Users", "AuditoriumScheduleSelectedSemesterId");
            CreateIndex("dbo.Users", "LecturerScheduleSelectedLecturerId");
            CreateIndex("dbo.Users", "LecturerScheduleSelectedStudyYearId");
            CreateIndex("dbo.Users", "LecturerScheduleSelectedSemesterId");
            DropColumn("dbo.Users", "SelectedBranchId");
            DropColumn("dbo.Users", "SelectedFacultyId");
            DropColumn("dbo.Users", "SelectedCourseId");
            DropColumn("dbo.Users", "SelectedBuildingId");
            DropColumn("dbo.Users", "SelectedStudyYearId");
            DropColumn("dbo.Users", "SelectedSemesterId");
            DropColumn("dbo.Users", "SelectedAuditoriumId");
            DropColumn("dbo.Users", "SelectedLecturerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "SelectedLecturerId", c => c.Int());
            AddColumn("dbo.Users", "SelectedAuditoriumId", c => c.Int());
            AddColumn("dbo.Users", "SelectedSemesterId", c => c.Int());
            AddColumn("dbo.Users", "SelectedStudyYearId", c => c.Int());
            AddColumn("dbo.Users", "SelectedBuildingId", c => c.Int());
            AddColumn("dbo.Users", "SelectedCourseId", c => c.Int());
            AddColumn("dbo.Users", "SelectedFacultyId", c => c.Int());
            AddColumn("dbo.Users", "SelectedBranchId", c => c.Int());
            DropIndex("dbo.Users", new[] { "LecturerScheduleSelectedSemesterId" });
            DropIndex("dbo.Users", new[] { "LecturerScheduleSelectedStudyYearId" });
            DropIndex("dbo.Users", new[] { "LecturerScheduleSelectedLecturerId" });
            DropIndex("dbo.Users", new[] { "AuditoriumScheduleSelectedSemesterId" });
            DropIndex("dbo.Users", new[] { "AuditoriumScheduleSelectedStudyYearId" });
            DropIndex("dbo.Users", new[] { "AuditoriumScheduleSelectedAuditoriumId" });
            DropIndex("dbo.Users", new[] { "CreatorSelectedAuditoriumId" });
            DropIndex("dbo.Users", new[] { "CreatorSelectedSemesterId" });
            DropIndex("dbo.Users", new[] { "CreatorSelectedStudyYearId" });
            DropIndex("dbo.Users", new[] { "CreatorSelectedBuildingId" });
            DropIndex("dbo.Users", new[] { "CreatorSelectedCourseId" });
            DropIndex("dbo.Users", new[] { "CreatorSelectedFacultyId" });
            DropIndex("dbo.Users", new[] { "CreatorSelectedBranchId" });
            DropForeignKey("dbo.Users", "LecturerScheduleSelectedSemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Users", "LecturerScheduleSelectedStudyYearId", "dbo.StudyYears");
            DropForeignKey("dbo.Users", "LecturerScheduleSelectedLecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.Users", "AuditoriumScheduleSelectedSemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Users", "AuditoriumScheduleSelectedStudyYearId", "dbo.StudyYears");
            DropForeignKey("dbo.Users", "AuditoriumScheduleSelectedAuditoriumId", "dbo.Auditoriums");
            DropForeignKey("dbo.Users", "CreatorSelectedAuditoriumId", "dbo.Auditoriums");
            DropForeignKey("dbo.Users", "CreatorSelectedSemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Users", "CreatorSelectedStudyYearId", "dbo.StudyYears");
            DropForeignKey("dbo.Users", "CreatorSelectedBuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Users", "CreatorSelectedCourseId", "dbo.Courses");
            DropForeignKey("dbo.Users", "CreatorSelectedFacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Users", "CreatorSelectedBranchId", "dbo.Branches");
            DropColumn("dbo.Users", "LecturerScheduleSelectedSemesterId");
            DropColumn("dbo.Users", "LecturerScheduleSelectedStudyYearId");
            DropColumn("dbo.Users", "LecturerScheduleSelectedLecturerId");
            DropColumn("dbo.Users", "AuditoriumScheduleSelectedSemesterId");
            DropColumn("dbo.Users", "AuditoriumScheduleSelectedStudyYearId");
            DropColumn("dbo.Users", "AuditoriumScheduleSelectedAuditoriumId");
            DropColumn("dbo.Users", "CreatorSelectedAuditoriumId");
            DropColumn("dbo.Users", "CreatorSelectedSemesterId");
            DropColumn("dbo.Users", "CreatorSelectedStudyYearId");
            DropColumn("dbo.Users", "CreatorSelectedBuildingId");
            DropColumn("dbo.Users", "CreatorSelectedCourseId");
            DropColumn("dbo.Users", "CreatorSelectedFacultyId");
            DropColumn("dbo.Users", "CreatorSelectedBranchId");
            CreateIndex("dbo.Users", "SelectedLecturerId");
            CreateIndex("dbo.Users", "SelectedAuditoriumId");
            CreateIndex("dbo.Users", "SelectedSemesterId");
            CreateIndex("dbo.Users", "SelectedStudyYearId");
            CreateIndex("dbo.Users", "SelectedBuildingId");
            CreateIndex("dbo.Users", "SelectedCourseId");
            CreateIndex("dbo.Users", "SelectedFacultyId");
            CreateIndex("dbo.Users", "SelectedBranchId");
            AddForeignKey("dbo.Users", "SelectedLecturerId", "dbo.Lecturers", "Id");
            AddForeignKey("dbo.Users", "SelectedAuditoriumId", "dbo.Auditoriums", "Id");
            AddForeignKey("dbo.Users", "SelectedSemesterId", "dbo.Semesters", "Id");
            AddForeignKey("dbo.Users", "SelectedStudyYearId", "dbo.StudyYears", "Id");
            AddForeignKey("dbo.Users", "SelectedBuildingId", "dbo.Buildings", "Id");
            AddForeignKey("dbo.Users", "SelectedCourseId", "dbo.Courses", "Id");
            AddForeignKey("dbo.Users", "SelectedFacultyId", "dbo.Faculties", "Id");
            AddForeignKey("dbo.Users", "SelectedBranchId", "dbo.Branches", "Id");
        }
    }
}
