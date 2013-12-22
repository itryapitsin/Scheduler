﻿using System;
using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class ScheduleInfoesToSpecialitiesSync: BaseSync
    {
        private DbConnection _connection;
        private string _commandPattern = @"
                INSERT INTO [dbo].[ScheduleInfoesToSpecialities]
                    ([ScheduleInfo_Id]
                    ,[Speciality_Id])
                VALUES
                    ({0}
                    ,{1});";
        public ScheduleInfoesToSpecialitiesSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                SELECT DISTINCT 
                    CES_ID, 
                    SPEC_CODE
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
            var schedulerEntities = SchedulerDatabase.ScheduleInfoes.Include("Specialities").ToList();
            var specialities = SchedulerDatabase.Specialities.ToList();
            var command = String.Empty;

            while (reader.Read())
            {
                var scheduleInfoId = reader.GetInt64(0);
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == scheduleInfoId);
                var id = reader.GetString(1);
                var speciality = specialities.FirstOrDefault(x => x.Code == id);
                if (schedulerEntity != null && speciality != null && !schedulerEntity.Specialities.Contains(speciality))
                {
                    schedulerEntity.Specialities.Add(speciality);
                    //SchedulerDatabase.Update(schedulerEntity);
                    command += String.Format(_commandPattern, schedulerEntity.Id, speciality.Id);
                }
            }

            if(!String.IsNullOrEmpty(command))
                SchedulerDatabase.RawSqlCommand(command);

            _connection.Close();
        }
    }
}