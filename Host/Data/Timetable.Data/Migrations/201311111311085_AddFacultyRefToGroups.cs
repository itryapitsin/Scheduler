namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFacultyRefToGroups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "FacultyId", c => c.Int(nullable: false, defaultValue: 1));
            AddForeignKey("dbo.Groups", "FacultyId", "dbo.Faculties", "Id", cascadeDelete: true);
            CreateIndex("dbo.Groups", "FacultyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Groups", new[] { "FacultyId" });
            DropForeignKey("dbo.Groups", "FacultyId", "dbo.Faculties");
            DropColumn("dbo.Groups", "FacultyId");
        }
    }
}
