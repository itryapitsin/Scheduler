namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditoriumOrdersAndExamsEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditoriumOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TutorialName = c.String(),
                        LecturerName = c.String(),
                        ThreadName = c.String(),
                        DayOfWeek = c.Int(nullable: false),
                        TimeId = c.Int(nullable: false),
                        AuditoriumId = c.Int(nullable: false),
                        AutoDelete = c.Boolean(nullable: false),
                        IIASKey = c.Long(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Times", t => t.TimeId, cascadeDelete: true)
                .ForeignKey("dbo.Auditoriums", t => t.AuditoriumId, cascadeDelete: true)
                .Index(t => t.TimeId)
                .Index(t => t.AuditoriumId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LecturerId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        TutorialId = c.Int(nullable: false),
                        AuditoriumId = c.Int(),
                        Time = c.DateTime(nullable: false),
                        IIASKey = c.Long(),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Tutorials", t => t.TutorialId, cascadeDelete: true)
                .ForeignKey("dbo.Auditoriums", t => t.AuditoriumId)
                .Index(t => t.LecturerId)
                .Index(t => t.GroupId)
                .Index(t => t.TutorialId)
                .Index(t => t.AuditoriumId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Exams", new[] { "AuditoriumId" });
            DropIndex("dbo.Exams", new[] { "TutorialId" });
            DropIndex("dbo.Exams", new[] { "GroupId" });
            DropIndex("dbo.Exams", new[] { "LecturerId" });
            DropIndex("dbo.AuditoriumOrders", new[] { "AuditoriumId" });
            DropIndex("dbo.AuditoriumOrders", new[] { "TimeId" });
            DropForeignKey("dbo.Exams", "AuditoriumId", "dbo.Auditoriums");
            DropForeignKey("dbo.Exams", "TutorialId", "dbo.Tutorials");
            DropForeignKey("dbo.Exams", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Exams", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.AuditoriumOrders", "AuditoriumId", "dbo.Auditoriums");
            DropForeignKey("dbo.AuditoriumOrders", "TimeId", "dbo.Times");
            DropTable("dbo.Exams");
            DropTable("dbo.AuditoriumOrders");
        }
    }
}
