namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTimetableEntityesToScheduleTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Schedules", "TypeId", c => c.Int());
            AddForeignKey("dbo.Schedules", "TypeId", "dbo.ScheduleTypes", "Id");
            CreateIndex("dbo.Schedules", "TypeId");
            

            Sql(@"
INSERT INTO [dbo].[ScheduleTypes]
           ([Name]
           ,[IsActive]
           ,[IsActual]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           ('Основное'
           ,1
           ,1
           ,GetDate()
           ,GetDate())

INSERT INTO [dbo].[ScheduleTypes]
           ([Name]
           ,[IsActive]
           ,[IsActual]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           ('Замена'
           ,1
           ,1
           ,GetDate()
           ,GetDate())
            ");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Schedules", new[] { "TypeId" });
            DropForeignKey("dbo.Schedules", "TypeId", "dbo.ScheduleTypes");
            DropColumn("dbo.Schedules", "TypeId");
            DropTable("dbo.ScheduleTypes");
        }
    }
}
