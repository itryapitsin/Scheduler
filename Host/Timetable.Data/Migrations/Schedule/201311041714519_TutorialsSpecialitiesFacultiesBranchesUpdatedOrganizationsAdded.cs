namespace Timetable.Data.Migrations.Schedule
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TutorialsSpecialitiesFacultiesBranchesUpdatedOrganizationsAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.Tutorials", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.Tutorials", "Speciality_Id", "dbo.Specialities");
            DropIndex("dbo.Departments", new[] { "Faculty_Id" });
            DropIndex("dbo.Tutorials", new[] { "Faculty_Id" });
            DropIndex("dbo.Tutorials", new[] { "Speciality_Id" });
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
            
            AddColumn("dbo.Branches", "OrganizationId", c => c.Int(nullable: false));
            AddColumn("dbo.Times", "Position", c => c.Int(nullable: false));
            AddForeignKey("dbo.Branches", "OrganizationId", "dbo.Organizations", "Id");
            CreateIndex("dbo.Branches", "OrganizationId");
            DropColumn("dbo.Departments", "FacultyId");
            DropColumn("dbo.Tutorials", "FacultyId");
            DropColumn("dbo.Tutorials", "SpecialityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tutorials", "SpecialityId", c => c.Int());
            AddColumn("dbo.Tutorials", "FacultyId", c => c.Int());
            AddColumn("dbo.Departments", "FacultyId", c => c.Int());
            DropIndex("dbo.DepartmentsToFaculties", new[] { "Faculty_Id" });
            DropIndex("dbo.DepartmentsToFaculties", new[] { "Department_Id" });
            DropIndex("dbo.SpecialitiesToTutorials", new[] { "Speciality_Id" });
            DropIndex("dbo.SpecialitiesToTutorials", new[] { "Tutorial_Id" });
            DropIndex("dbo.FacultiesToTutorials", new[] { "Faculty_Id" });
            DropIndex("dbo.FacultiesToTutorials", new[] { "Tutorial_Id" });
            DropIndex("dbo.Branches", new[] { "OrganizationId" });
            DropForeignKey("dbo.DepartmentsToFaculties", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.DepartmentsToFaculties", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.SpecialitiesToTutorials", "Speciality_Id", "dbo.Specialities");
            DropForeignKey("dbo.SpecialitiesToTutorials", "Tutorial_Id", "dbo.Tutorials");
            DropForeignKey("dbo.FacultiesToTutorials", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.FacultiesToTutorials", "Tutorial_Id", "dbo.Tutorials");
            DropForeignKey("dbo.Branches", "OrganizationId", "dbo.Organizations");
            DropColumn("dbo.Branches", "OrganizationId");
            DropTable("dbo.DepartmentsToFaculties");
            DropTable("dbo.SpecialitiesToTutorials");
            DropTable("dbo.FacultiesToTutorials");
            DropTable("dbo.Organizations");
            CreateIndex("dbo.Tutorials", "SpecialityId");
            CreateIndex("dbo.Tutorials", "FacultyId");
            CreateIndex("dbo.Departments", "FacultyId");
            AddForeignKey("dbo.Tutorials", "SpecialityId", "dbo.Specialities", "Id");
            AddForeignKey("dbo.Tutorials", "FacultyId", "dbo.Faculties", "Id");
            AddForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties", "Id");
        }
    }
}
