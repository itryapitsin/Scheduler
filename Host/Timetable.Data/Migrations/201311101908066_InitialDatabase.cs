namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        BranchId = c.Int(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.ScheduleInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LecturerId = c.Int(nullable: false),
                        TutorialTypeId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        SubgroupCount = c.Int(nullable: false),
                        HoursPerWeek = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TutorialId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        StudyYearId = c.Int(nullable: false),
                        Semester = c.Int(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tutorials", t => t.TutorialId, cascadeDelete: true)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .ForeignKey("dbo.TutorialTypes", t => t.TutorialTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.StudyYears", t => t.StudyYearId, cascadeDelete: true)
                .Index(t => t.TutorialId)
                .Index(t => t.LecturerId)
                .Index(t => t.TutorialTypeId)
                .Index(t => t.DepartmentId)
                .Index(t => t.StudyYearId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        Code = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tutorials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        CourseId = c.Int(nullable: false),
                        SpecialityId = c.Int(nullable: false),
                        StudentsCount = c.Int(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Specialities", t => t.SpecialityId)
                .ForeignKey("dbo.Groups", t => t.Parent_Id)
                .Index(t => t.CourseId)
                .Index(t => t.SpecialityId)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Auditoriums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(),
                        Capacity = c.Int(),
                        Info = c.String(),
                        BuildingId = c.Int(nullable: false),
                        AuditoriumTypeId = c.Int(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId)
                .ForeignKey("dbo.AuditoriumTypes", t => t.AuditoriumTypeId)
                .Index(t => t.BuildingId)
                .Index(t => t.AuditoriumTypeId);
            
            CreateTable(
                "dbo.TutorialTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        ShortName = c.String(),
                        Info = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuditoriumTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Pattern = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuditoriumId = c.Int(),
                        DayOfWeek = c.Int(nullable: false),
                        PeriodId = c.Int(nullable: false),
                        ScheduleInfoId = c.Int(nullable: false),
                        WeekTypeId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        AutoDelete = c.Boolean(nullable: false),
                        TimetableEntityId = c.Int(),
                        SubGroup = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auditoriums", t => t.AuditoriumId)
                .ForeignKey("dbo.Times", t => t.PeriodId, cascadeDelete: true)
                .ForeignKey("dbo.ScheduleInfoes", t => t.ScheduleInfoId, cascadeDelete: true)
                .ForeignKey("dbo.WeekTypes", t => t.WeekTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TimetableEntities", t => t.TimetableEntityId)
                .Index(t => t.AuditoriumId)
                .Index(t => t.PeriodId)
                .Index(t => t.ScheduleInfoId)
                .Index(t => t.WeekTypeId)
                .Index(t => t.TimetableEntityId);
            
            CreateTable(
                "dbo.Times",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Start = c.Time(nullable: false),
                        End = c.Time(nullable: false),
                        Position = c.Int(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeekTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimetableEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lecturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lastname = c.String(),
                        Firstname = c.String(),
                        Middlename = c.String(),
                        Contacts = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudyYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartYear = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OrganizationId = c.Int(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScheduleInfoesToFaculties",
                c => new
                    {
                        ScheduleInfo_Id = c.Int(nullable: false),
                        Faculty_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ScheduleInfo_Id, t.Faculty_Id })
                .ForeignKey("dbo.ScheduleInfoes", t => t.ScheduleInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id, cascadeDelete: true)
                .Index(t => t.ScheduleInfo_Id)
                .Index(t => t.Faculty_Id);
            
            CreateTable(
                "dbo.ScheduleInfoesToCourses",
                c => new
                    {
                        ScheduleInfo_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ScheduleInfo_Id, t.Course_Id })
                .ForeignKey("dbo.ScheduleInfoes", t => t.ScheduleInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.ScheduleInfo_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.FacultiesToTutorials",
                c => new
                    {
                        Tutorial_Id = c.Int(nullable: false),
                        Faculty_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tutorial_Id, t.Faculty_Id })
                .ForeignKey("dbo.Tutorials", t => t.Tutorial_Id, cascadeDelete: true)
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id, cascadeDelete: true)
                .Index(t => t.Tutorial_Id)
                .Index(t => t.Faculty_Id);
            
            CreateTable(
                "dbo.SpecialitiesToTutorials",
                c => new
                    {
                        Tutorial_Id = c.Int(nullable: false),
                        Speciality_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tutorial_Id, t.Speciality_Id })
                .ForeignKey("dbo.Tutorials", t => t.Tutorial_Id, cascadeDelete: true)
                .ForeignKey("dbo.Specialities", t => t.Speciality_Id, cascadeDelete: true)
                .Index(t => t.Tutorial_Id)
                .Index(t => t.Speciality_Id);
            
            CreateTable(
                "dbo.ScheduleInfoesToSpecialities",
                c => new
                    {
                        ScheduleInfo_Id = c.Int(nullable: false),
                        Speciality_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ScheduleInfo_Id, t.Speciality_Id })
                .ForeignKey("dbo.ScheduleInfoes", t => t.ScheduleInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Specialities", t => t.Speciality_Id, cascadeDelete: true)
                .Index(t => t.ScheduleInfo_Id)
                .Index(t => t.Speciality_Id);
            
            CreateTable(
                "dbo.ScheduleInfoesToGroups",
                c => new
                    {
                        ScheduleInfo_Id = c.Int(nullable: false),
                        Group_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ScheduleInfo_Id, t.Group_Id })
                .ForeignKey("dbo.ScheduleInfoes", t => t.ScheduleInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.ScheduleInfo_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.TutorialTypesToAuditoriums",
                c => new
                    {
                        Auditorium_Id = c.Int(nullable: false),
                        TutorialType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Auditorium_Id, t.TutorialType_Id })
                .ForeignKey("dbo.Auditoriums", t => t.Auditorium_Id, cascadeDelete: true)
                .ForeignKey("dbo.TutorialTypes", t => t.TutorialType_Id, cascadeDelete: true)
                .Index(t => t.Auditorium_Id)
                .Index(t => t.TutorialType_Id);
            
            CreateTable(
                "dbo.ScheduleInfoesToAuditoriums",
                c => new
                    {
                        ScheduleInfo_Id = c.Int(nullable: false),
                        Auditorium_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ScheduleInfo_Id, t.Auditorium_Id })
                .ForeignKey("dbo.ScheduleInfoes", t => t.ScheduleInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Auditoriums", t => t.Auditorium_Id, cascadeDelete: true)
                .Index(t => t.ScheduleInfo_Id)
                .Index(t => t.Auditorium_Id);
            
            CreateTable(
                "dbo.LecturersToDepartments",
                c => new
                    {
                        Lecturer_Id = c.Int(nullable: false),
                        Department_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Lecturer_Id, t.Department_Id })
                .ForeignKey("dbo.Lecturers", t => t.Lecturer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Department_Id, cascadeDelete: true)
                .Index(t => t.Lecturer_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.LecturersToPositions",
                c => new
                    {
                        Lecturer_Id = c.Int(nullable: false),
                        Position_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Lecturer_Id, t.Position_Id })
                .ForeignKey("dbo.Lecturers", t => t.Lecturer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: true)
                .Index(t => t.Lecturer_Id)
                .Index(t => t.Position_Id);
            
            CreateTable(
                "dbo.SpecialitiesToFaculties",
                c => new
                    {
                        Faculty_Id = c.Int(nullable: false),
                        Speciality_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Faculty_Id, t.Speciality_Id })
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id, cascadeDelete: true)
                .ForeignKey("dbo.Specialities", t => t.Speciality_Id, cascadeDelete: true)
                .Index(t => t.Faculty_Id)
                .Index(t => t.Speciality_Id);
            
            CreateTable(
                "dbo.DepartmentsToFaculties",
                c => new
                    {
                        Department_Id = c.Int(nullable: false),
                        Faculty_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Department_Id, t.Faculty_Id })
                .ForeignKey("dbo.Departments", t => t.Department_Id, cascadeDelete: true)
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id, cascadeDelete: true)
                .Index(t => t.Department_Id)
                .Index(t => t.Faculty_Id);

            Sql(@"
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Аудитория'
           ,1
           ,'ауд'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Кабинет'
           ,1
           ,'каб'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Дисплейный класс'
           ,1
           ,'дисп'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Лаборатория'
           ,1
           ,'лаб'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Комната'
           ,1
           ,'комната'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Зал'
           ,1
           ,'зал'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Отдел'
           ,1
           ,'отдел'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Лингафонный класс'
           ,1
           ,'лингаф'
           ,getdate()
           ,getdate()
           ,null)

");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DepartmentsToFaculties", new[] { "Faculty_Id" });
            DropIndex("dbo.DepartmentsToFaculties", new[] { "Department_Id" });
            DropIndex("dbo.SpecialitiesToFaculties", new[] { "Speciality_Id" });
            DropIndex("dbo.SpecialitiesToFaculties", new[] { "Faculty_Id" });
            DropIndex("dbo.LecturersToPositions", new[] { "Position_Id" });
            DropIndex("dbo.LecturersToPositions", new[] { "Lecturer_Id" });
            DropIndex("dbo.LecturersToDepartments", new[] { "Department_Id" });
            DropIndex("dbo.LecturersToDepartments", new[] { "Lecturer_Id" });
            DropIndex("dbo.ScheduleInfoesToAuditoriums", new[] { "Auditorium_Id" });
            DropIndex("dbo.ScheduleInfoesToAuditoriums", new[] { "ScheduleInfo_Id" });
            DropIndex("dbo.TutorialTypesToAuditoriums", new[] { "TutorialType_Id" });
            DropIndex("dbo.TutorialTypesToAuditoriums", new[] { "Auditorium_Id" });
            DropIndex("dbo.ScheduleInfoesToGroups", new[] { "Group_Id" });
            DropIndex("dbo.ScheduleInfoesToGroups", new[] { "ScheduleInfo_Id" });
            DropIndex("dbo.ScheduleInfoesToSpecialities", new[] { "Speciality_Id" });
            DropIndex("dbo.ScheduleInfoesToSpecialities", new[] { "ScheduleInfo_Id" });
            DropIndex("dbo.SpecialitiesToTutorials", new[] { "Speciality_Id" });
            DropIndex("dbo.SpecialitiesToTutorials", new[] { "Tutorial_Id" });
            DropIndex("dbo.FacultiesToTutorials", new[] { "Faculty_Id" });
            DropIndex("dbo.FacultiesToTutorials", new[] { "Tutorial_Id" });
            DropIndex("dbo.ScheduleInfoesToCourses", new[] { "Course_Id" });
            DropIndex("dbo.ScheduleInfoesToCourses", new[] { "ScheduleInfo_Id" });
            DropIndex("dbo.ScheduleInfoesToFaculties", new[] { "Faculty_Id" });
            DropIndex("dbo.ScheduleInfoesToFaculties", new[] { "ScheduleInfo_Id" });
            DropIndex("dbo.Branches", new[] { "OrganizationId" });
            DropIndex("dbo.Schedules", new[] { "TimetableEntityId" });
            DropIndex("dbo.Schedules", new[] { "WeekTypeId" });
            DropIndex("dbo.Schedules", new[] { "ScheduleInfoId" });
            DropIndex("dbo.Schedules", new[] { "PeriodId" });
            DropIndex("dbo.Schedules", new[] { "AuditoriumId" });
            DropIndex("dbo.Auditoriums", new[] { "AuditoriumTypeId" });
            DropIndex("dbo.Auditoriums", new[] { "BuildingId" });
            DropIndex("dbo.Groups", new[] { "Parent_Id" });
            DropIndex("dbo.Groups", new[] { "SpecialityId" });
            DropIndex("dbo.Groups", new[] { "CourseId" });
            DropIndex("dbo.ScheduleInfoes", new[] { "StudyYearId" });
            DropIndex("dbo.ScheduleInfoes", new[] { "DepartmentId" });
            DropIndex("dbo.ScheduleInfoes", new[] { "TutorialTypeId" });
            DropIndex("dbo.ScheduleInfoes", new[] { "LecturerId" });
            DropIndex("dbo.ScheduleInfoes", new[] { "TutorialId" });
            DropIndex("dbo.Faculties", new[] { "BranchId" });
            DropForeignKey("dbo.DepartmentsToFaculties", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.DepartmentsToFaculties", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.SpecialitiesToFaculties", "Speciality_Id", "dbo.Specialities");
            DropForeignKey("dbo.SpecialitiesToFaculties", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.LecturersToPositions", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.LecturersToPositions", "Lecturer_Id", "dbo.Lecturers");
            DropForeignKey("dbo.LecturersToDepartments", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.LecturersToDepartments", "Lecturer_Id", "dbo.Lecturers");
            DropForeignKey("dbo.ScheduleInfoesToAuditoriums", "Auditorium_Id", "dbo.Auditoriums");
            DropForeignKey("dbo.ScheduleInfoesToAuditoriums", "ScheduleInfo_Id", "dbo.ScheduleInfoes");
            DropForeignKey("dbo.TutorialTypesToAuditoriums", "TutorialType_Id", "dbo.TutorialTypes");
            DropForeignKey("dbo.TutorialTypesToAuditoriums", "Auditorium_Id", "dbo.Auditoriums");
            DropForeignKey("dbo.ScheduleInfoesToGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.ScheduleInfoesToGroups", "ScheduleInfo_Id", "dbo.ScheduleInfoes");
            DropForeignKey("dbo.ScheduleInfoesToSpecialities", "Speciality_Id", "dbo.Specialities");
            DropForeignKey("dbo.ScheduleInfoesToSpecialities", "ScheduleInfo_Id", "dbo.ScheduleInfoes");
            DropForeignKey("dbo.SpecialitiesToTutorials", "Speciality_Id", "dbo.Specialities");
            DropForeignKey("dbo.SpecialitiesToTutorials", "Tutorial_Id", "dbo.Tutorials");
            DropForeignKey("dbo.FacultiesToTutorials", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.FacultiesToTutorials", "Tutorial_Id", "dbo.Tutorials");
            DropForeignKey("dbo.ScheduleInfoesToCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.ScheduleInfoesToCourses", "ScheduleInfo_Id", "dbo.ScheduleInfoes");
            DropForeignKey("dbo.ScheduleInfoesToFaculties", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.ScheduleInfoesToFaculties", "ScheduleInfo_Id", "dbo.ScheduleInfoes");
            DropForeignKey("dbo.Branches", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Schedules", "TimetableEntityId", "dbo.TimetableEntities");
            DropForeignKey("dbo.Schedules", "WeekTypeId", "dbo.WeekTypes");
            DropForeignKey("dbo.Schedules", "ScheduleInfoId", "dbo.ScheduleInfoes");
            DropForeignKey("dbo.Schedules", "PeriodId", "dbo.Times");
            DropForeignKey("dbo.Schedules", "AuditoriumId", "dbo.Auditoriums");
            DropForeignKey("dbo.Auditoriums", "AuditoriumTypeId", "dbo.AuditoriumTypes");
            DropForeignKey("dbo.Auditoriums", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Groups", "Parent_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "SpecialityId", "dbo.Specialities");
            DropForeignKey("dbo.Groups", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ScheduleInfoes", "StudyYearId", "dbo.StudyYears");
            DropForeignKey("dbo.ScheduleInfoes", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ScheduleInfoes", "TutorialTypeId", "dbo.TutorialTypes");
            DropForeignKey("dbo.ScheduleInfoes", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.ScheduleInfoes", "TutorialId", "dbo.Tutorials");
            DropForeignKey("dbo.Faculties", "BranchId", "dbo.Branches");
            DropTable("dbo.DepartmentsToFaculties");
            DropTable("dbo.SpecialitiesToFaculties");
            DropTable("dbo.LecturersToPositions");
            DropTable("dbo.LecturersToDepartments");
            DropTable("dbo.ScheduleInfoesToAuditoriums");
            DropTable("dbo.TutorialTypesToAuditoriums");
            DropTable("dbo.ScheduleInfoesToGroups");
            DropTable("dbo.ScheduleInfoesToSpecialities");
            DropTable("dbo.SpecialitiesToTutorials");
            DropTable("dbo.FacultiesToTutorials");
            DropTable("dbo.ScheduleInfoesToCourses");
            DropTable("dbo.ScheduleInfoesToFaculties");
            DropTable("dbo.Organizations");
            DropTable("dbo.Branches");
            DropTable("dbo.StudyYears");
            DropTable("dbo.Positions");
            DropTable("dbo.Lecturers");
            DropTable("dbo.TimetableEntities");
            DropTable("dbo.WeekTypes");
            DropTable("dbo.Times");
            DropTable("dbo.Schedules");
            DropTable("dbo.AuditoriumTypes");
            DropTable("dbo.Buildings");
            DropTable("dbo.TutorialTypes");
            DropTable("dbo.Auditoriums");
            DropTable("dbo.Groups");
            DropTable("dbo.Tutorials");
            DropTable("dbo.Specialities");
            DropTable("dbo.Courses");
            DropTable("dbo.ScheduleInfoes");
            DropTable("dbo.Faculties");
            DropTable("dbo.Departments");
        }
    }
}
