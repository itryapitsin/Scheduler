namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedSelectedBranchId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "SelectedBranch_Id", newName: "SelectedBranchId");
            DropColumn("dbo.Users", "SelectedBranchhId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "SelectedBranchhId", c => c.Int());
            RenameColumn(table: "dbo.Users", name: "SelectedBranchId", newName: "SelectedBranch_Id");
        }
    }
}
