using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Подготовка базы данных после синхронизации")]
    public class AfterSync : BaseSync
    {
         
        public override async void Sync()
        {
            var command = @"

UPDATE [dbo].[AuditoriumTypes] SET [Training] = 'true';
GO

INSERT [dbo].[AuditoriumTypesToUsers] ([User_Id], [AuditoriumType_Id]) VALUES (1, 2)
GO

INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (1, 11)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (23, 11)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (60, 11)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (94, 11)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (119, 11)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (135, 11)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (149, 11)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (154, 11)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (15, 22)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (43, 22)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (79, 22)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (105, 22)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (132, 22)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (144, 22)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (15, 28)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (43, 28)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (79, 28)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (105, 28)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (132, 28)
GO
INSERT [dbo].[BuildingsToTimes] ([Time_Id], [Building_Id]) VALUES (144, 28)
GO
";
            SchedulerDatabase.RawSqlCommand(command);
        }
    }
}
