namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoursesRefToBranches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BranchesToCourses",
                c => new
                    {
                        Branch_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Branch_Id, t.Course_Id })
                .ForeignKey("dbo.Branches", t => t.Branch_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Branch_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BranchesToCourses", new[] { "Course_Id" });
            DropIndex("dbo.BranchesToCourses", new[] { "Branch_Id" });
            DropForeignKey("dbo.BranchesToCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.BranchesToCourses", "Branch_Id", "dbo.Branches");
            DropTable("dbo.BranchesToCourses");
        }
    }
}
