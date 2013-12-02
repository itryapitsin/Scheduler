namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersAndUserRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            Sql(@"
INSERT INTO [dbo].[UserRoles]
           ([Name]
           ,[Type]
           ,[IsActual]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           ('Администратор'
           ,2
           ,1
           ,getdate()
           ,getdate())");

            Sql(@"
INSERT INTO [dbo].[Users]
           ([Login]
           ,[Password]
           ,[RoleId]
           ,[IsActual]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           ('Admin'
           ,'Flvbybcnhfnjh'
           ,1
           ,1
           ,getdate()
           ,getdate())");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
        }
    }
}
