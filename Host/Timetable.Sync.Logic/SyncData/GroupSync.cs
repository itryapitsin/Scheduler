﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    [Description("Синхронизация групп")]
    public class GroupSync: BaseSync
    {
        private string _insertQueryPattern = @"
            INSERT INTO [dbo].[Groups]
                       ([Code]                       
                       ,[StudentsCount]
                       ,[IsActual]
                       ,[CreatedDate]
                       ,[UpdatedDate]
                       ,[IIASKey]
                       ,[Parent_Id]
                       ,[StudyTypeId])
                 VALUES
                       ('{0}'                      
                       ,0
                       ,1
                       ,GetDate()
                       ,GetDate()
                       ,{1}
                       ,null
                       ,{2});";
        private string _updateQueryPattern = @"
                UPDATE [dbo].[Groups]
                   SET [Code] = '{0}'
                      ,[UpdatedDate] = GetDate()
                      ,[StudyTypeId] = {2}
                 WHERE Id = {1};";

        public override async void Sync()
        {
            var task1 = Task.Factory.StartNew(() => IIASContext.GetGroups().ToList());
            var task2 = Task.Factory.StartNew(() => SchedulerDatabase.Groups.ToList());

            Task.WaitAll(task1, task2);

            var iiasEntities = await task1;
            var schedulerEntities = await task2;
            var studyTypes = SchedulerDatabase.StudyTypes.ToList();
            var command = String.Empty;

            foreach (var iiasEntity in iiasEntities)
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == iiasEntity.Id);
                var studyType = studyTypes.FirstOrDefault(x => x.IIASKey == iiasEntity.StudyTypeId);
                if(studyType == null)
                    continue;

                if (schedulerEntity == null)
                {
                    command += string.Format(
                        _insertQueryPattern,
                        iiasEntity.Code,
                        iiasEntity.Id,
                        studyType.Id);
                }
                else
                {
                    command += string.Format(
                        _updateQueryPattern,
                        iiasEntity.Code,
                        iiasEntity.Id,
                        studyType.Id);
                }
            }

            SchedulerDatabase.RawSqlCommand(command);
        }
    }
}
