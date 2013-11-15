namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTimetableEntityes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "TimetableEntityId", "dbo.TimetableEntities");
            DropIndex("dbo.Schedules", new[] { "TimetableEntityId" });
            DropColumn("dbo.Schedules", "TimetableEntityId");
            DropTable("dbo.TimetableEntities");
        }
        
        public override void Down()
        {
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

            AddColumn("dbo.Schedules", "TimetableEntityId", c => c.Int());
            CreateIndex("dbo.Schedules", "TimetableEntityId");
            AddForeignKey("dbo.Schedules", "TimetableEntityId", "dbo.TimetableEntities", "Id");
        }
    }
}
