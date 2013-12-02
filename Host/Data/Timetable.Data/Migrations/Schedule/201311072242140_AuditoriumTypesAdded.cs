namespace Timetable.Data.Migrations.Schedule
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditoriumTypesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditoriumTypes", "Pattern", c => c.String());

            Sql(@"
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Аудитория'
           ,1
           ,'ауд'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Кабинет'
           ,1
           ,'каб'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Дисплейный класс'
           ,1
           ,'дисп'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Лаборатория'
           ,1
           ,'лаб'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Комната'
           ,1
           ,'комната'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Зал'
           ,1
           ,'зал'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Отдел'
           ,1
           ,'отдел'
           ,getdate()
           ,getdate()
           ,null)
INSERT INTO [dbo].[AuditoriumTypes]
           ([Name]
           ,[IsActual]
           ,[Pattern]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[IIASKey])
     VALUES
           ('Лингафонный класс'
           ,1
           ,'лингаф'
           ,getdate()
           ,getdate()
           ,null)

");
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditoriumTypes", "Pattern");
        }
    }
}
