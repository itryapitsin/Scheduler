namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudyTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IIASKey = c.Long(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Groups", "StudyTypeId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Groups", "StudyTypeId", "dbo.StudyTypes", "Id", cascadeDelete: true);
            CreateIndex("dbo.Groups", "StudyTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Groups", new[] { "StudyTypeId" });
            DropForeignKey("dbo.Groups", "StudyTypeId", "dbo.StudyTypes");
            DropColumn("dbo.Groups", "StudyTypeId");
            DropTable("dbo.StudyTypes");
        }
    }
}
