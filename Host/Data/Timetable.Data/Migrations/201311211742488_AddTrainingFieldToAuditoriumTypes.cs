namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainingFieldToAuditoriumTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditoriumTypes", "Training", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditoriumTypes", "Training");
        }
    }
}
