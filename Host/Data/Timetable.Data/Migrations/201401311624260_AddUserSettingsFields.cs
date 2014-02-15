namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSettingsFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SettingsDataEditSelectedBuildingId", c => c.Int());
            AddColumn("dbo.Users", "SettingsDataEditSelectedTabId", c => c.Int());
            AddForeignKey("dbo.Users", "SettingsDataEditSelectedBuildingId", "dbo.Buildings", "Id");
            CreateIndex("dbo.Users", "SettingsDataEditSelectedBuildingId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "SettingsDataEditSelectedBuildingId" });
            DropForeignKey("dbo.Users", "SettingsDataEditSelectedBuildingId", "dbo.Buildings");
            DropColumn("dbo.Users", "SettingsDataEditSelectedTabId");
            DropColumn("dbo.Users", "SettingsDataEditSelectedBuildingId");
        }
    }
}
