﻿using System;
using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class ScheduleInfoesToCoursesSync: BaseSync
    {
        private DbConnection _connection;

        public ScheduleInfoesToCoursesSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                SELECT DISTINCT 
                    CES_ID AS Id, 
                    NAME_KURS
                FROM
                    SDMS.V_UPL_RASP
                WHERE
                    (HOURS_WEEK IS NOT NULL) 
                    AND (PCARD_ID IS NOT NULL) 
                    AND (EW_ID IS NOT NULL) 
                    AND (DIS_CODE IS NOT NULL) 
                    AND (UBU_ID IS NOT NULL) 
                    AND (GR_UBU_ID > 0) 
                    AND (GR_UBU_ID IS NOT NULL) 
                    AND (KURS_CODE > 0) 
                    AND (KURS_CODE IS NOT NULL) 
                    AND (SPEC_CODE IS NOT NULL) 
                    AND (FACULT_CODE IS NOT NULL)";
            var reader = cmd.ExecuteReader();
            var schedulerEntities = SchedulerDatabase.ScheduleInfoes.Include("Courses").ToList();

            while (reader.Read())
            {
                var scheduleInfoId = reader.GetInt64(0);
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == scheduleInfoId);
                var id = reader.GetString(1);
                var course = SchedulerDatabase.Courses.First(x => x.Name == id);
                if (schedulerEntity != null && course != null && !schedulerEntity.Courses.Contains(course))
                {
                    schedulerEntity.Courses.Add(course);
                    SchedulerDatabase.Update(schedulerEntity);
                }
            }

            _connection.Close();
        }
    }
}
