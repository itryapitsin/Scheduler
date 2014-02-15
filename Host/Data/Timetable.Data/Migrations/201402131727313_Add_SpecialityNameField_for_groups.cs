namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SpecialityNameField_for_groups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "SpecialityName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "SpecialityName");
        }
    }
}
