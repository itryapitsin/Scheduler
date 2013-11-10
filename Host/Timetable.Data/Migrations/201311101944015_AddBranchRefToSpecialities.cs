namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBranchRefToSpecialities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specialities", "BranchId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Specialities", "BranchId", "dbo.Branches", "Id", cascadeDelete: true);
            CreateIndex("dbo.Specialities", "BranchId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Specialities", new[] { "BranchId" });
            DropForeignKey("dbo.Specialities", "BranchId", "dbo.Branches");
            DropColumn("dbo.Specialities", "BranchId");
        }
    }
}
