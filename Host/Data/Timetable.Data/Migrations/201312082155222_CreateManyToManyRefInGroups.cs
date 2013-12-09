namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateManyToManyRefInGroups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Groups", "SpecialityId", "dbo.Specialities");
            DropForeignKey("dbo.Groups", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Groups", new[] { "CourseId" });
            DropIndex("dbo.Groups", new[] { "SpecialityId" });
            DropIndex("dbo.Groups", new[] { "FacultyId" });
            CreateTable(
                "dbo.GroupsToCourses",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Course_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.GroupsToSpecialities",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        Speciality_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Speciality_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Specialities", t => t.Speciality_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Speciality_Id);
            
            CreateTable(
                "dbo.GroupsToFaculties",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        Faculty_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Faculty_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Faculty_Id);
            
            DropColumn("dbo.Groups", "CourseId");
            DropColumn("dbo.Groups", "SpecialityId");
            DropColumn("dbo.Groups", "FacultyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "FacultyId", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "SpecialityId", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "CourseId", c => c.Int(nullable: false));
            DropIndex("dbo.GroupsToFaculties", new[] { "Faculty_Id" });
            DropIndex("dbo.GroupsToFaculties", new[] { "Group_Id" });
            DropIndex("dbo.GroupsToSpecialities", new[] { "Speciality_Id" });
            DropIndex("dbo.GroupsToSpecialities", new[] { "Group_Id" });
            DropIndex("dbo.GroupsToCourses", new[] { "Course_Id" });
            DropIndex("dbo.GroupsToCourses", new[] { "Group_Id" });
            DropForeignKey("dbo.GroupsToFaculties", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.GroupsToFaculties", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.GroupsToSpecialities", "Speciality_Id", "dbo.Specialities");
            DropForeignKey("dbo.GroupsToSpecialities", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.GroupsToCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.GroupsToCourses", "Group_Id", "dbo.Groups");
            DropTable("dbo.GroupsToFaculties");
            DropTable("dbo.GroupsToSpecialities");
            DropTable("dbo.GroupsToCourses");
            CreateIndex("dbo.Groups", "FacultyId");
            CreateIndex("dbo.Groups", "SpecialityId");
            CreateIndex("dbo.Groups", "CourseId");
            AddForeignKey("dbo.Groups", "FacultyId", "dbo.Faculties", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Groups", "SpecialityId", "dbo.Specialities", "Id");
            AddForeignKey("dbo.Groups", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
