namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudyTypeFieldToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreatorSelectedStudyTypeId", c => c.Int());
            AddForeignKey("dbo.Users", "CreatorSelectedStudyTypeId", "dbo.StudyTypes", "Id");
            CreateIndex("dbo.Users", "CreatorSelectedStudyTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "CreatorSelectedStudyTypeId" });
            DropForeignKey("dbo.Users", "CreatorSelectedStudyTypeId", "dbo.StudyTypes");
            DropColumn("dbo.Users", "CreatorSelectedStudyTypeId");
        }
    }
}
