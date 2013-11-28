namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditoriumScheduleSelectedAuditoriumTypesAddedToUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditoriumTypesToUsers",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        AuditoriumType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.AuditoriumType_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.AuditoriumTypes", t => t.AuditoriumType_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.AuditoriumType_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AuditoriumTypesToUsers", new[] { "AuditoriumType_Id" });
            DropIndex("dbo.AuditoriumTypesToUsers", new[] { "User_Id" });
            DropForeignKey("dbo.AuditoriumTypesToUsers", "AuditoriumType_Id", "dbo.AuditoriumTypes");
            DropForeignKey("dbo.AuditoriumTypesToUsers", "User_Id", "dbo.Users");
            DropTable("dbo.AuditoriumTypesToUsers");
        }
    }
}
