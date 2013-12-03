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
           ('���������'
           ,1
           ,'���'
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
           ('�������'
           ,1
           ,'���'
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
           ('���������� �����'
           ,1
           ,'����'
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
           ('�����������'
           ,1
           ,'���'
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
           ('�������'
           ,1
           ,'�������'
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
           ('���'
           ,1
           ,'���'
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
           ('�����'
           ,1
           ,'�����'
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
           ('����������� �����'
           ,1
           ,'������'
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
