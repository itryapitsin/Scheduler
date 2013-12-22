namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationLogsTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    CREATE TABLE [dbo].[ApplicationLogs](
	                    [Date] [datetime] NOT NULL,
	                    [Thread] [nvarchar](255) NOT NULL,
	                    [Level] [nvarchar](50) NOT NULL,
	                    [Logger] [nvarchar](255) NOT NULL,
	                    [Message] [nvarchar](4000) NULL,
	                    [Exception] [nvarchar](4000) NOT NULL
                    ) ON [PRIMARY]");
        }
        
        public override void Down()
        {
            Sql(@"DROP TABLE [dbo].[ApplicationLogs]");
        }
    }
}
